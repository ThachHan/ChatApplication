using AutoMapper;
using ChatApp.Core.Services.Interfaces;
using ChatApp.Domain.Entities;
using ChatApp.Domain.Models.Conversation;
using ChatApp.Domain.Models.Message;
using ChatApp.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Chat.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ConversationController : ControllerBase
    {
        private readonly IConversationService _conversationService;
        private readonly IMessageService _messageService;
        private readonly IAppUserService _appUserService;
        private readonly IMapper _mapper;
        private readonly IHubContext<ChatHub> _hubContext;

        public ConversationController(
            IConversationService conversationService,
            IMessageService messageService,
            IAppUserService appUserService,
            IMapper mapper,
            IHubContext<ChatHub> hubContext)
        {
            _conversationService = conversationService;
            _messageService = messageService;
            _appUserService = appUserService;
            _mapper = mapper;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllConversationAsync()
        {
            var conversations = await _conversationService.GetAllAsync();

            var result = _mapper.Map<List<ConversationModel>>(conversations);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var conversation = await _conversationService.GetByIdAsync(id);

            var result = _mapper.Map<ConversationModel>(conversation);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(ConversationCreateModel model)
        {
            var conversation = await _conversationService.GetByNameAsync(model.Name);
            if (conversation != null)
                return BadRequest("Invalid conversation name.");

            var appUser = await _appUserService.GetByUserNameAsync(User.Identity.Name);
            conversation = await _conversationService.CreateAsync(model.Name, appUser.Id);

            var createdRoom = _mapper.Map<Conversation, ConversationModel>(conversation);
            await _hubContext.Clients.All.SendAsync("addChatRoom", createdRoom);

            return Ok(createdRoom);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var conversation = await _conversationService.GetByIdAsync(id);
            if (conversation == null)
                return BadRequest("Invalid conversation.");

            await _conversationService.DeleteAsync(id);

            await _hubContext.Clients.All.SendAsync("removeChatRoom", id);
            await _hubContext.Clients.Group(conversation.Name).SendAsync("onRoomDeleted");

            return Ok();
        }


        [HttpGet("{id}/messages")]
        public async Task<IActionResult> GetConversationMessagesAsync(Guid id)
        {
            var conversation = await _conversationService.GetByIdAsync(id);
            if (conversation == null)
                return BadRequest("Invalid conversation.");

            // TODO: add paging
            var messages = await _messageService.GetByConversationIdAsync(id);

            var result = _mapper.Map<List<MessageModel>>(messages);

            return Ok(result);
        }
    }
}
