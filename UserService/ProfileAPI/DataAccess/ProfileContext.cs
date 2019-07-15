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
<<<<<<< HEAD
        //public DbSet<Music> Music { get; set; }
=======
        
>>>>>>> b0a9ab9b1201c6fc41c5f15b905d14d1b858fcd4
    }
}
