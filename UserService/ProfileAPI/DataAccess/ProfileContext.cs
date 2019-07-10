using Microsoft.EntityFrameworkCore;
using ProfileAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileAPI.DataAccess
{
    public class ProfileContext : DbContext
    {
        public ProfileContext(DbContextOptions<ProfileContext> options)
           : base(options)
        {
            Database.EnsureCreated();
            //Database.Migrate();
        }

        public DbSet<Profile> Profiles { get; set; }
        
    }
}
