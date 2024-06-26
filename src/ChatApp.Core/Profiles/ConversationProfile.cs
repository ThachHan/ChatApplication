using AutoMapper;
using ChatApp.Domain.Entities;
using ChatApp.Domain.Models.Conversation;

namespace ChatApp.Core.Profiles
{
    public class ConversationProfile : Profile
    {
        public ConversationProfile()
        {
            CreateMap<Conversation, ConversationModel>();
        }
    }
}
