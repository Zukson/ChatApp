using ChatApp.Dto;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp.Data
{
    public class DataContext : IdentityDbContext
    {
        public DbSet<ChatRoomDto> ChatRooms { get; set; }
        public DbSet<MessageDto> Messages { get; set; }
        public DbSet<ChatUserDto> ChatUsers { get; set; }
        public DbSet<RefreshTokenDto> RefreshTokens{ get; set; }

       
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }
    }
}
