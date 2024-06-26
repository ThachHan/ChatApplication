using AutoMapper;
using ChatApp.Domain.Entities;
using ChatApp.Domain.Models.Message;

namespace ChatApp.Core.Profiles
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<Message, MessageModel>()
                .ForMember(x => x.FromUserName, b => b.MapFrom(n => n.Sender != null ? n.Sender.UserName : string.Empty));
        }
    }
}
