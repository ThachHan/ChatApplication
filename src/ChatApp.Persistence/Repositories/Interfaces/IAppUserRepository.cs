using ChatApp.Domain.Entities;

namespace ChatApp.Persistence.Repositories.Interfaces;

public interface IAppUserRepository
{
    Task<AppUser?> GetByUserNameAsync(string userName);
}
