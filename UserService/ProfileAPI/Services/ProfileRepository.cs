using ProfileAPI.DataAccess;
using ProfileAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileAPI.Services
{
    public class ProfileRepository :IProfileRepository
    {
        private ProfileContext context;

        public ProfileRepository(ProfileContext context)
        {
            this.context = context;
        }
           
        public IEnumerable<String> GetUserBooks(int userId)
        { 
            IEnumerable<Profile> profiles = this.context.Profile.Where(x => x.UserId == userId);
            List<string> books = new List<string>();
            foreach (var p in profiles)
            {
                books.Add(p.BookId);
            }
            //IEnumerable<String> p = (IEnumerable<String>)this.context.Profile.Where(x => x.UserId == userId);
            return (IEnumerable<String>)books;
        }

        public Profile Create(Profile profile)
        {
            this.context.Profile.Add(profile);
            this.context.SaveChanges();
            return profile;
        }

        public Profile Update(Profile profile)
        {
            this.context.Profile.Update(profile);
            this.context.SaveChanges();
            return profile;
        }

        public void Delete (Profile profile)
        {
            this.context.Profile.Remove(profile);
            this.context.SaveChanges();
        }

    }
}
