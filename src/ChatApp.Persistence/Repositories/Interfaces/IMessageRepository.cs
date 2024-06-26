using ChatApp.Domain.Entities;

namespace ChatApp.Persistence.Repositories.Interfaces;

public interface IMessageRepository : IGenericRepository<Message>
{
    Task<List<Message>> GetByConversationIdAsync(Guid conversationId);
}
