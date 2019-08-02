using IdentityServer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;


namespace IdentityServer
{
    public class UserConnection : DbContext
    {
        public UserConnection(DbContextOptions<UserConnection> options)
                : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> User_Role { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
