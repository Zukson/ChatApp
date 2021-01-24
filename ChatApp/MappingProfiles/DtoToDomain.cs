using AutoMapper;
using ChatApp.Domain;
using ChatApp.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.MappingProfiles
{
    public class DtoToDomain :Profile
    {
        public DtoToDomain()
        {
            CreateMap<ChatUserDto, ChatUser>();
            CreateMap<MessageDto, Message>();
            CreateMap<ChatRoomDto, ChatRoom>()
                .ForMember(dst => dst.Messages, opt => opt.MapFrom(x => x.Messages.Select(message => new Message
                {
                    MessageId = message.MessageId,
                    SendDate = message.SendDate,
                    Text = message.Text,
                    SenderName = message.SenderName

                }
                     ))).ForMember
                     (dst => dst.Users, opt => opt.MapFrom(x => x.Users.Select(user => new ChatUser 
                     { Name = user.Name,
                     AvatarPath = user.AvatarPath }
                     )));

        }
    }
}
