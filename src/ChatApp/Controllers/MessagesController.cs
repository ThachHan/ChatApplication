using AutoMapper;
using ChatApp.Core.Services.Interfaces;
using ChatApp.Domain.Entities;
using ChatApp.Domain.Models.Message;
using ChatApp.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Chat.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IAppUserService _appUserService;
        private readonly IConversationService _conversationService;
        private readonly IMapper _mapper;
        private readonly IHubContext<ChatHub> _hubContext;

        public MessagesController(
            IMessageService messageService,
            IAppUserService appUserService,
            IConversationService conversationService,
            IMapper mapper,
            IHubContext<ChatHub> hubContext)
        {
            _messageService = messageService;
            _appUserService = appUserService;
            _conversationService = conversationService;
            _mapper = mapper;
            _hubContext = hubContext;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var message = await _messageService.GetByIdAsync(id);
            if (message == null)
                return NotFound();

            var result = _mapper.Map<Message, MessageModel>(message);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(MessageCreateModel model)
        {
            var appUser = await _appUserService.GetByUserNameAsync(User.Identity.Name);
            var conversation = await _conversationService.GetByIdAsync(model.ConversationId);
            if (conversation == null)
                return BadRequest();

            var message = await _messageService.CreateAsync(model.Content, model.ConversationId, appUser.Id);

            var createdMessage = _mapper.Map<Message, MessageModel>(message);
            await _hubContext.Clients.Group(conversation.Name).SendAsync("newMessage", createdMessage);

            return Ok(createdMessage);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var message = await _messageService.GetByIdAsync(id);
            if (message == null)
                return NotFound();

            await _messageService.DeleteAsync(id);

            await _hubContext.Clients.All.SendAsync("removeChatMessage", message.Id);

            return Ok();
        }
    }
}
