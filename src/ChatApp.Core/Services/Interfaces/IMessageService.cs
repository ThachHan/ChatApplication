using ChatApp.Domain.Entities;

namespace ChatApp.Core.Services.Interfaces
{
    public interface IMessageService
    {
        Task<List<Message>> GetAllAsync();
        Task<List<Message>> GetByConversationIdAsync(Guid conversationId);
        Task<Message> GetByIdAsync(Guid id);
        Task<Message> CreateAsync(string content, Guid conversationId, Guid senderId);
        Task DeleteAsync(Guid id);
    }
}
