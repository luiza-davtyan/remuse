using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileAPI.Entities
{
    public class Profile
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public int UserId { get; set; }
        [Required]
        public string BookId { get; set; }

        public Profile(int userId , string bookId)
        {
            this.BookId = bookId;
            this.UserId = userId;
        } 
    }
}
