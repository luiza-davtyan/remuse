using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RemuseWebApplication.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int RoleId { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public List<Book> Books { get; set; }
        public List<Music> Musics { get; set; }
    }
}
