using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileAPI.Entities
{
    public class Profile
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string BookId { get; set; }

        public string Song { get; set; }

        public Profile(int userId , string bookId, string song)
        {
            this.BookId = bookId;
            this.UserId = userId;
            this.Song = song;
        }
    
    }
}
