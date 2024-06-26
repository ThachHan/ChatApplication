using ChatApp.Common.Models;
using ChatApp.Domain.Entities;

namespace ChatApp.Core.Services.Interfaces;

public interface IAppUserService
{
    Task<AppUser> GetByUserNameAsync(string userName);
}
