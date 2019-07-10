using BookService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Models
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int RoleId { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        //public List<BookDTO> Books { get; set; }
    }
}
