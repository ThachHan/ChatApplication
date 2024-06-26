using ChatApp.Core.Services.Interfaces;
using ChatApp.Domain.Entities;
using ChatApp.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Core.Services
{
    public class ConversationService : IConversationService
    {
        private readonly IConversationRepository _conversationRepository;

        public ConversationService(IConversationRepository conversationRepository)
        {
            _conversationRepository = conversationRepository;
        }
        public async Task<Conversation> CreateAsync(string name, Guid creatorId)
        {
            return await _conversationRepository.CreateAsync(new Conversation()
            {
                Name = name,
                CreatorId = creatorId
            });
        }

        public async Task DeleteAsync(Guid id)
        {
            var conversation = await GetByIdAsync(id);
            _conversationRepository.Delete(conversation);
        }

        public async Task<List<Conversation>> GetAllAsync()
        {
            return await _conversationRepository.GetAllAsync().ToListAsync();
        }

        public async Task<Conversation> GetByIdAsync(Guid id)
        {
            return await _conversationRepository.GetByIdAsync(id) ?? throw new InvalidOperationException();
        }

        public async Task<Conversation?> GetByNameAsync(string name)
        {
            return await _conversationRepository.GetByNameAsync(name);
        }
    }
}
