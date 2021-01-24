using AutoMapper;
using ChatApp.Domain;
using ChatApp.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.MappingProfiles
{
    public class DomainToDto : Profile
    {
        public DomainToDto()
        {
            CreateMap<ChatUser, ChatUserDto>();
            CreateMap<Message, MessageDto>();
            CreateMap<ChatRoom, ChatRoomDto>()
               .ForMember(dst => dst.Messages, opt => opt.MapFrom(x => x.Messages.Select(message => new MessageDto
               {
                   MessageId = message.MessageId,
                   SendDate = message.SendDate,
                   Text = message.Text,
                   SenderName = message.SenderName

               }
                    ))).ForMember
                    (dst => dst.Users, opt => opt.MapFrom(x => x.Users.Select(user => new ChatUserDto
                    {
                        Name = user.Name,
                        AvatarPath = user.AvatarPath
                    }
                    )));
        }
    }
}
