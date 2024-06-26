using ChatApp.Core.Services.Interfaces;
using ChatApp.Domain.Entities;
using ChatApp.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Core.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;

        public MessageService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }
        public async Task<Message> CreateAsync(string content, Guid conversationId, Guid senderId)
        {
            return await _messageRepository.CreateAsync(new Message()
            {
                Content = content,
                SenderId = senderId,
                ConversationId = conversationId,
            });
        }

        public async Task DeleteAsync(Guid id)
        {
            var message = await GetByIdAsync(id);
            _messageRepository.Delete(message);
        }

        public async Task<List<Message>> GetAllAsync()
        {
            return await _messageRepository.GetAllAsync().ToListAsync();
        }

        public async Task<List<Message>> GetByConversationIdAsync(Guid conversationId)
        {
            return await _messageRepository.GetByConversationIdAsync(conversationId);
        }

        public async Task<Message> GetByIdAsync(Guid id)
        {
            return await _messageRepository.GetByIdAsync(id) ?? throw new InvalidOperationException();
        }
    }
}
