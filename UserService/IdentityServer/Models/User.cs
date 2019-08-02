using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        //public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public byte[] Picture { get; set; }
        //public string Role { get; set; }


        public User(int id, string name, string surname,/* DateTime dateOfBirth,*/ string email,
                    string username, string password)//, string role)
        {
            this.Id = id;
            this.Name = name;
            this.Surname = surname;
           // this.DateOfBirth = dateOfBirth;
            this.Email = email;
            this.Username = username;
            this.Password = password;
           // this.Role = role;
        }
    }
}
