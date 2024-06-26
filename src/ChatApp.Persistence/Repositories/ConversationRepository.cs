using ChatApp.Domain.Entities;
using ChatApp.Persistence.DbManager;
using ChatApp.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Persistence.Repositories;

public class ConversationRepository(AppDbContext database) : GenericRepository<Conversation>(database), IConversationRepository
{
    public async Task<Conversation?> GetByNameAsync(string name)
    {
        return await GetAllAsync()
            .Where(x => x.Name == name)
            .FirstOrDefaultAsync();
    }
}
