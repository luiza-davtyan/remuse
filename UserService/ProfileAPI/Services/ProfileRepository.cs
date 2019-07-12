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

        public IEnumerable<Profile> GetUserBooks(int userId)
        {
            return this.context.Profiles.Where(x => x.UserId == userId);
        }

        public Profile Create(Profile profile)
        {
            this.context.Profiles.Add(profile);
            this.context.SaveChanges();
            return profile;
        }
    }
}
