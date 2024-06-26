using ChatApp.Domain.Entities;

namespace ChatApp.Persistence.Repositories.Interfaces;

public interface IConversationRepository : IGenericRepository<Conversation>
{
    Task<Conversation?> GetByNameAsync(string name);
}
