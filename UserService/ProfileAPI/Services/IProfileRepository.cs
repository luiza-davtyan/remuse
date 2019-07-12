using ProfileAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileAPI.Services
{
    public interface IProfileRepository
    {
        IEnumerable<Profile> GetUserBooks(int userId);
        Profile Create(Profile profile);
    }
}
