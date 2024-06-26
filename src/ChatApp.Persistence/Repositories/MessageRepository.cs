using ChatApp.Domain.Entities;
using ChatApp.Persistence.DbManager;
using ChatApp.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Persistence.Repositories;

public class MessageRepository(AppDbContext database) : GenericRepository<Message>(database), IMessageRepository
{
    public async Task<List<Message>> GetByConversationIdAsync(Guid conversationId)
    {
        return await GetAllAsync()
            .Where(x => x.ConversationId == conversationId)
            .Include(x => x.Sender)
            .ToListAsync();
    }
}
