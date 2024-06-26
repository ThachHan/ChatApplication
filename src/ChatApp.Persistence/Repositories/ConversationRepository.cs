using ChatApp.Domain.Entities;
using ChatApp.Persistence.DbManager;
using ChatApp.Persistence.Repositories.Interfaces;

namespace ChatApp.Persistence.Repositories;

public class ConversationRepository(AppDbContext database) : GenericRepository<Conversation>(database), IConversationRepository
{
}
