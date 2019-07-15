﻿using ProfileAPI.DataAccess;
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
            IEnumerable<String> p = (IEnumerable<String>)this.context.Profiles.Where(x => x.UserId == userId);
            return p;
        }

        public Profile Create(Profile profile)
        {
            this.context.Profiles.Add(profile);
            this.context.SaveChanges();
            return profile;
        }

        public Profile Update(Profile profile)
        {
            this.context.Profiles.Update(profile);
            this.context.SaveChanges();
            return profile;
        }

        public void Delete (Profile profile)
        {
            this.context.Profiles.Remove(profile);
            this.context.SaveChanges();
        }

    }
}
