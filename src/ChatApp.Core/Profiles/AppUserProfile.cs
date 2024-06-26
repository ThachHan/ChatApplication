using AutoMapper;
using ChatApp.Domain.Entities;
using ChatApp.Domain.Models;

namespace ChatApp.Core.Profiles
{
    public class AppUserProfile : Profile
    {
        public AppUserProfile()
        {
            CreateMap<AppUser, AppUserModel>();
        }
    }
}
