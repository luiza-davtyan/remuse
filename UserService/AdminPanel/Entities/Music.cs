using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RemuseWebApplication.Entities
{
    public class Music
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Singer { get; set; }
        public string Genre { get; set; }
    }
}
