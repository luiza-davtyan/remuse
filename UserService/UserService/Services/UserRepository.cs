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
            return context.Users.First(s => s.Id == userId);
        }

        public void DeleteUserById(int userId)
        {
            var user = context.Users.Single(x => x.Id == userId);
            if(user != null)
            context.Users.Remove(user);
            //context.Users.
        }

        //ToDo
        public void AddUser(string firstName, string lastName, DateTime dateOfBirth, string email, string username, string password)
        {
            User user = new User(firstName, lastName, dateOfBirth, email, username, password);
            context.Users.Add(user);
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

        //ToDo
        bool IUserRepository.AddPicture(byte[] pic)
        {
            throw new NotImplementedException();
        }
    }
}
