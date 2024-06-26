using ChatApp.Persistence;
using ChatApp.Persistence.DbManager;
using ChatApp.Persistence.Repositories.Interfaces;
using ChatApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Persistence.Repositories;

public class AppUserRepository : IAppUserRepository
{
    private readonly AppDbContext _dbContext;

    public AppUserRepository(AppDbContext database)
    {
        _dbContext = database;
    }

    public async Task<AppUser?> GetByUserNameAsync(string userName)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(x => x.UserName == userName);
    }
}
