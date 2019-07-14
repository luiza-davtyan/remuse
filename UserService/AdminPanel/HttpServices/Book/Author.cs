using System;

namespace BookService.Models
{
    public class Author
    {
        /// <summary>
        /// Id of the author. It is required and is automatically generated.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// First name of the author.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of the author.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Date of birth of the author.
        /// </summary>
        public DateTimeOffset DateOfBirth { get; set; }
    }
}
