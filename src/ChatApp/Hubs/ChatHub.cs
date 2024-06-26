using AutoMapper;
using ChatApp.Core.Services.Interfaces;
using ChatApp.Domain.Entities;
using ChatApp.Domain.Models;
using ChatApp.Domain.Models.Message;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;

namespace ChatApp.Hubs
{
    [Authorize]
    public partial class ChatHub : Hub
    {
        public readonly List<AppUserModel> Connections = [];
        private readonly Dictionary<string, string> ConnectionsMap = [];

        private readonly IAppUserService _appUserService;
        private readonly IMapper _mapper;

        public ChatHub(IAppUserService appUserService, IMapper mapper)
        {
            _appUserService = appUserService;
            _mapper = mapper;
        }

        public async Task SendPrivate(string receiverName, string message)
        {
            if (ConnectionsMap.TryGetValue(receiverName, out string userId))
            {
                var sender = Connections.First(u => u.UserName == IdentityName);

                if (!string.IsNullOrEmpty(message.Trim()))
                {
                    // Build the message
                    var messageModel = new MessageModel()
                    {
                        Content = MessageRegex().Replace(message, string.Empty),
                        FromUserName = sender.UserName,
                        Room = "",
                        Timestamp = DateTime.Now
                    };

                    // Send the message
                    await Clients.Client(userId).SendAsync("newMessage", messageModel);
                    await Clients.Caller.SendAsync("newMessage", messageModel);
                }
            }
        }

        public async Task Join(string conversationName)
        {
            try
            {
                var user = Connections.Find(u => u.UserName == IdentityName);
                if (user != null && user.CurrentRoom != conversationName)
                {
                    if (!string.IsNullOrEmpty(user.CurrentRoom))
                        await Clients.OthersInGroup(user.CurrentRoom).SendAsync("removeUser", user);

                    await Leave(user.CurrentRoom);
                    await Groups.AddToGroupAsync(Context.ConnectionId, conversationName);
                    user.CurrentRoom = conversationName;

                    await Clients.OthersInGroup(conversationName).SendAsync("addUser", user);
                }
            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("onError", "You failed to join the conversation room!" + ex.Message);
            }
        }

        public async Task Leave(string conversationName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, conversationName);
        }

        public IEnumerable<AppUserModel> GetUsers(string conversationName)
        {
            return Connections.Where(u => u.CurrentRoom == conversationName).ToList();
        }

        public override async Task OnConnectedAsync()
        {
            try
            {
                var appUser = await _appUserService.GetByUserNameAsync(IdentityName);
                var userViewModel = _mapper.Map<AppUser, AppUserModel>(appUser);
                userViewModel.Device = GetDevice();
                userViewModel.CurrentRoom = "";

                if (!Connections.Exists(u => u.UserName == IdentityName))
                {
                    Connections.Add(userViewModel);
                    ConnectionsMap.Add(IdentityName, Context.ConnectionId);
                }

                await Clients.Caller.SendAsync("getProfileInfo", userViewModel);
            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("onError", "OnConnected:" + ex.Message);
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            try
            {
                var user = Connections.First(u => u.UserName == IdentityName);
                Connections.Remove(user);

                await Clients.OthersInGroup(user.CurrentRoom).SendAsync("removeUser", user);

                ConnectionsMap.Remove(user.UserName);
            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("onError", "OnDisconnected: " + ex.Message);
            }

            await base.OnDisconnectedAsync(exception);
        }

        private string IdentityName
        {
            get { return Context.User.Identity.Name; }
        }

        private string GetDevice()
        {
            var device = Context.GetHttpContext().Request.Headers["Device"].ToString();
            if (!string.IsNullOrEmpty(device) && (device.Equals("Desktop") || device.Equals("Mobile")))
                return device;

            return "Web";
        }

        [GeneratedRegex(@"<.*?>")]
        private static partial Regex MessageRegex();
    }
}
