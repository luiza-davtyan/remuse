using IdentityServer.Models;
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
        public Task<User> GetUserByID(int userId)
        {
            var task = new Task<User>(() =>
            {
                var userObject = context.Users.FirstOrDefault<User>(user => user.Id == userId);
                var userRoleObject = context.User_Role.FirstOrDefault<User_Role>(userRole => userRole.UserID == userId);
                if (userRoleObject != null)
                {
                    var roleObject = context.Role.FirstOrDefault<Role>(role => role.Id == userRoleObject.RoleID);
                    if (roleObject != null)
                    {
                        userObject.Role = roleObject.Permission;
                    }
                }
                //var userObject = context.Users.Join(context.User_Role, (user => user.Id), (user_role => user_role.UserID), ((user, user_role) => new { Users = user, User_Role = user_role }))
                //.Join(context.Role, (uur => uur.User_Role.RoleID), (r => r.Id), (uur, r) => new { uur, r })
                //.Where(blabla => blabla.uur.User_Role.UserID == userId)
                //.Select(u => new User {
                    
                //});

                
                return userObject;
            });

            task.Start();

            return task;
        }

        /// <summary>
        /// Get user by username.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Task<User> GetUserByUsername(string username)
        {
            var task = new Task<User>(() =>
            {
                var userObject = context.Users.FirstOrDefault<User>(user => user.Username.Equals(username));
                var userRoleObject = context.User_Role.FirstOrDefault<User_Role>(userRole => userRole.UserID == userObject.Id);
                if (userRoleObject != null)
                {
                    var roleObject = context.Role.FirstOrDefault<Role>(role => role.Id == userRoleObject.RoleID);
                    if (roleObject != null)
                    {
                        userObject.Role = roleObject.Permission;
                    }
                }
                return userObject;
            });

            task.Start();

            return task;
        }
    }
}
