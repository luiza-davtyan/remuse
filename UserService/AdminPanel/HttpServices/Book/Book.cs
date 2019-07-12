using Newtonsoft.Json;
using System.Collections.Generic;

namespace BookService.Models
{
    public class Book
    {
        /// <summary>
        /// Id of the book. It is required and is automatically generated.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Title of the book. It is required.
        /// </summary>
        [JsonProperty("Title")]
        public string Title { get; set; }

        /// <summary>
        /// Content of the book. It is required.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Description of the book.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Author Id.
        /// </summary>
        public string AuthorId { get; set; }

        /// <summary>
        /// Year of the book being published.
        /// </summary>
        public int Year { get; set; }


        public virtual List<int> GenreIds { get; set; }
    }
}

