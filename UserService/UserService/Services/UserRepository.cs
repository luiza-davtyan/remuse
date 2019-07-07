using System;
using UserService.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace UserService.Services
{
    public class UserRepository : IUserRepository
    {
        private UserConnection context;

        public UserRepository(UserConnection context)
        {
            this.context = context;
        }

        public IEnumerable<User> GetUsers()
        {
            return this.context.Users.OrderBy(a => a.Name).ThenBy(a => a.Surname).ToList();
        }

        public User GetUserByID(int userId)
        {
            return context.Users.FirstOrDefault<User>(user => user.Id == userId);
        }

        public User GetUserByUsername(string username)
        {
            return context.Users.FirstOrDefault<User>(user => user.Username.Equals(username));
        }

        public void DeleteUserById(int userId)
        {
            var user = GetUserByID(userId);
            if(user != null)
                context.Users.Remove(user);
        }

        public void AddUser(User newUser)
        {
            User user = new User(newUser.Name, newUser.Surname,
                                 newUser.DateOfBirth, newUser.Email, newUser.Username, newUser.Password);
            this.context.Users.Add(user);
            //this.context.SaveChanges();
        }

        public void Update(User newUser)
        {
            var user = context.Users.Find(newUser.Id);
            if (user == null)
            {
                return;
            }
            context.Entry(user).CurrentValues.SetValues(newUser);
        }

        //TODO
        bool IUserRepository.AddPicture(byte[] pic)
        {
            throw new NotImplementedException();
        }
    }
}
