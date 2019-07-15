using ProfileAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileAPI.Services
{
    public interface IProfileRepository
    {
        IEnumerable<String> GetUserBooks(int userId);
        Profile Create(Profile profile);
        Profile Update(Profile profile);
        void Delete(Profile profile);
    }
}
