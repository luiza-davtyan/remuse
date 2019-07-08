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

        /// <summary>
        /// Public construvtor.
        /// </summary>
        /// <param name="context"></param>
        public UserRepository(UserConnection context)
        {
            this.context = context;
        }

        /// <summary>
        /// Get list of all users.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<User> GetUsers()
        {
            var users = this.context.Users.OrderBy(a => a.Name).ThenBy(a => a.Surname).ToList();
            return users;
        }

        /// <summary>
        /// Get user by Id.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public User GetUserByID(int userId)
        {
            return context.Users.FirstOrDefault<User>(user => user.Id == userId);
        }

        /// <summary>
        /// Get user by username.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public User GetUserByUsername(string username)
        {
            return context.Users.FirstOrDefault<User>(user => user.Username.Equals(username));
        }

        /// <summary>
        /// Delete user by Id.
        /// </summary>
        /// <param name="userId"></param>
        public void DeleteUserById(int userId)
        {
            var user = GetUserByID(userId);
            if(user != null)
                context.Users.Remove(user);
            context.SaveChanges();
        }

        /// <summary>
        /// Add new user.
        /// </summary>
        /// <param name="newUser"></param>
        public void AddUser(User newUser)
        {
            // User user = new User(newUser.Name, newUser.Surname,
            //                    newUser.DateOfBirth, newUser.Email, newUser.Username, newUser.Password);
            //newUser.Password = getHashSha256(newUser.Password);
            this.context.Users.Add(newUser);
            this.context.SaveChanges();
        }

        /// <summary>
        /// Update user by Id with the given object.
        /// </summary>
        /// <param name="newUser"></param>
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
        /// <summary>
        /// Add picture for user.
        /// </summary>
        /// <param name="pic"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool IUserRepository.AddPicture(byte[] pic, int userId)
        {
            throw new NotImplementedException();
        }
    }
}
