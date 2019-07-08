using Microsoft.EntityFrameworkCore;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using UserService.Models;

namespace UserService
{
    public class UserConnection : DbContext
    {
        public UserConnection(DbContextOptions<UserConnection> options)
                : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
