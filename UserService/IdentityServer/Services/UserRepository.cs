using IdentityServer.Models;
using IdentityServer.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Services
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
        public Task<UserDTO> GetUserByID(int userId)
        {
            var task = new Task<UserDTO>(() =>
            {
                UserDTO userDTO = new UserDTO();
                var userObject = context.Users.FirstOrDefault<User>(user => user.Id == userId);
                var userRoleObject = context.User_Role.FirstOrDefault<User_Role>(userRole => userRole.UserID == userId);
                if (userRoleObject != null)
                {
                    var roleObject = context.Roles.FirstOrDefault<Role>(role => role.Id == userRoleObject.RoleID);
                    if (roleObject != null)
                    {
                        userDTO = new UserDTO(userObject.Id, userObject.Name, userObject.Surname, userObject.DateOfBirth, userObject.Email, userObject.Username, userObject.Password, roleObject.Permission);
                    }
                }
                return userDTO;
            });

            task.Start();

            return task;
        }

        /// <summary>
        /// Get user by username.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Task<UserDTO> GetUserByUsername(string username)
        {
            var task = new Task<UserDTO>(() =>
            {
                UserDTO userDTO = new UserDTO();
                var userObject = context.Users.FirstOrDefault<User>(user => user.Username.Equals(username));
                var userRoleObject = context.User_Role.FirstOrDefault<User_Role>(userRole => userRole.UserID == userObject.Id);
                if (userRoleObject != null)
                {
                    var roleObject = context.Roles.FirstOrDefault<Role>(role => role.Id == userRoleObject.RoleID);
                    if (roleObject != null)
                    {
                       userDTO = new UserDTO(userObject.Id, userObject.Name, userObject.Surname, userObject.DateOfBirth,userObject.Email, userObject.Username, userObject.Password, roleObject.Permission);
                    }
                }
                return userDTO;
            });

            task.Start();

            return task;
        }
    }
}
