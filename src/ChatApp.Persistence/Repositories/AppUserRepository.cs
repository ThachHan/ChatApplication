using ChatApp.Persistence;
using ChatApp.Persistence.DbManager;
using ChatApp.Persistence.Repositories.Interfaces;
using ChatApp.Domain.Entities;

namespace ChatApp.Persistence.Repositories;

public class AppUserRepository(AppDbContext database) : GenericRepository<AppUser>(database), IAppUserRepository
{
}
