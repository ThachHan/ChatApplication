using ChatApp.Domain.Entities;

namespace ChatApp.Core.Services.Interfaces
{
    public interface IConversationService
    {
        Task<List<Conversation>> GetAllAsync();
        Task<Conversation> GetByIdAsync(Guid id);
        Task<Conversation?> GetByNameAsync(string name);
        Task<Conversation> CreateAsync(string name, Guid creatorId);
        Task DeleteAsync(Guid id);
    }
}
