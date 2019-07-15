using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public byte[] Picture { get; set; }
        public string Role { get; set; }

        public UserDTO()
        {

        }

        public UserDTO(int id, string name, string surname, string email,
                    string username, string password, string role)
        {
            this.Id = id;
            this.Name = name;
            this.Surname = surname;
            this.Email = email;
            this.Username = username;
            this.Password = password;
            this.Role = role;
        }
    }
}
