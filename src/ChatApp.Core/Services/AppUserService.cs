using ChatApp.Common.Models;
using ChatApp.Core.Services.Interfaces;
using ChatApp.Domain.Entities;
using ChatApp.Persistence.Repositories.Interfaces;

namespace ChatApp.Core.Services;
public class AppUserService : IAppUserService
{
    private readonly IAppUserRepository _appUserRepository;

    public AppUserService(IAppUserRepository appUserRepository) => _appUserRepository = appUserRepository;

    public async Task<AppUser> GetByUserNameAsync(string userName)
    {
        return await _appUserRepository.GetByUserNameAsync(userName) ?? throw new InvalidDataException("AppUser not found.");
    }
}
