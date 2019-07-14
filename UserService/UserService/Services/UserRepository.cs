using System.Collections.Generic;
using System.Linq;
using UserService.Models;

namespace UserService.Services
{
    public class UserRepository : IUserRepository
    {
        private UserConnection context;

        /// <summary>
        /// A helper class object.
        /// </summary>
        private readonly Helper helper;

        /// <summary>
        /// Public construvtor.
        /// </summary>
        /// <param name="context"></param>
        public UserRepository(UserConnection context)
        {
            this.context = context;
            this.helper = new Helper();
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
        /// Get user_role by userID and role(default it is user).
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public User_Role GetUser_RoleByID(int userId)
        {
            return context.User_Role.FirstOrDefault<User_Role>(user => user.UserID == userId && user.RoleID == 2);
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
            var user_role = GetUser_RoleByID(userId);
            if (user_role != null)
                context.User_Role.Remove(user_role);
            context.SaveChanges();
            var user = GetUserByID(userId);
            if (user != null)
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

            User_Role user_role = new User_Role();
            user_role.RoleID = 2;
            user_role.UserID = newUser.Id;
            this.context.User_Role.Add(user_role);
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
            this.context.SaveChanges();
        }

        //TODO
        /// <summary>
        /// Change user's picture.
        /// </summary>
        /// <param name="pic"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        void IUserRepository.ChangePicture(byte[] pic, int userId)
        {
            User user = GetUserByID(userId);
            user.Picture = pic;
            context.SaveChanges();
        }

        /// <summary>
        /// Get user by username and password.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public User GetUserByUsername(string username, string password)
        {
            var user = context.Users.FirstOrDefault<User>(currUser => currUser.Username.Equals(username));
            if (!user.Password.Equals(helper.getHashSha256(password)))
            {
                return null;
            }
            return user;
        }

        /// <summary>
        /// Get user by email.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public User GetUserByEmail(string email)
        {
            return context.Users.FirstOrDefault<User>(user => user.Email.Equals(email));
        }
    }
}
