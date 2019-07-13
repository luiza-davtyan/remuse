using BookService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Models
{
    public class User
    {
        // prop
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public byte[] Picture { get; set; }


        public User(string name, string surname, string email,
                    string username, string password)
        {
            this.Name = name;
            this.Surname = surname;
            this.Email = email;
            this.Username = username;
            this.Password = password;
        }
    }
}
