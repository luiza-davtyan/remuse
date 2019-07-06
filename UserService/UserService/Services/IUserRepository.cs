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
        void AddUser(string firstName, string lastName, DateTime dateOfBirth, string email,
                         string username, string password);
        //bool UpdateUserName(int userId, string newName);
        //bool UpdateUserLastName(int userId, string newLastName);
        //bool UpdateUserEmail(int userId, string newEmail);
        //bool UpdateUserPicture(int userId, byte[] newPic);
        //bool UpdateUserDateOfBirth(int userId, DateTime newDateOfBirth);
        //bool UpdateUserPassword(int userId, string newPassword);
        void Update(User newUser);
        IEnumerable<User> GetUsers();
        User GetUserByID(int userId);
        bool AddPicture(byte[] pic);


        // User GetUserByUsername(string username);
        // void CreateUser();
        //void SignUp(); //TODO
        //User SignIn(string login, string password);
        //void ChangePassword(string newPassword);

    }
}
