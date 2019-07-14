using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Models;

namespace UserService.Services
{
    public interface IUserRepository
    {
        /// <summary>
        /// Delete user by Id.
        /// </summary>
        /// <param name="userId"></param>
        void DeleteUserById(int userId);
        /// <summary>
        /// Add user in db.
        /// </summary>
        /// <param name="user"></param>
        void AddUser(User user);
        /// <summary>
        /// Update user by Id with the given object.
        /// </summary>
        /// <param name="newUser"></param>
        void Update(User newUser);
        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns></returns>
        IEnumerable<User> GetUsers();
        /// <summary>
        /// Get user by Id.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        User GetUserByID(int userId);
        /// <summary>
        /// Get user by username.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        User GetUserByUsername(string username);
        /// <summary>
        /// Get user by email.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        User GetUserByEmail(string email);
        /// <summary>
        /// Change user's picture.
        /// </summary>
        /// <param name="pic"></param>
        /// <returns></returns>
        void ChangePicture(byte[] pic, int userId);
        /// <summary>
        /// Get user by username and password.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
       // User GetUserByEmail(string username);
    }
}
