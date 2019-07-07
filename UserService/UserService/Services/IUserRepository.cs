using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Models;

namespace UserService.Services
{
    public interface IUserRepository
    {
        void DeleteUserById(int userId);
        void AddUser(User user);
        void Update(User newUser);
        IEnumerable<User> GetUsers();
        User GetUserByID(int userId);
        User GetUserByUsername(string username);
        bool AddPicture(byte[] pic);
    }
}
