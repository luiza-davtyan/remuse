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
           
        /// <summary>
        /// Gets user's books by user id.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Creats user profile
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        public Profile Create(Profile profile)
        {
            this.context.Profile.Add(profile);
            try
            {
                this.context.SaveChanges();
            }
            catch { }
            return profile;
        }

        /// <summary>
        /// Updates user's profile
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        public Profile Update(Profile profile)
        {
            this.context.Profile.Update(profile);
            this.context.SaveChanges();
            return profile;
        }

        /// <summary>
        /// Delete user profile.
        /// </summary>
        /// <param name="profile"></param>
        public void Delete (Profile profile)
        {
            this.context.Profile.Remove(profile);
            this.context.SaveChanges();
        }

    }
}
