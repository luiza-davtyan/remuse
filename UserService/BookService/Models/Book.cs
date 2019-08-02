using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BookService.Models
{
    public class Book
    {
        /// <summary>
        /// Id of the book. It is required and is automatically generated.
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// Title of the book. It is required.
        /// </summary>
        [JsonProperty("Title")]
        [BsonRequired]
        public string Title { get; set; }

        /// <summary>
        /// Content of the book. It is required.
        /// </summary>
        [BsonRequired]
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

        public string Path { get; set; }

        /// <summary>
        /// List of genre id's of the book.
        /// </summary>
        public virtual List<ObjectId> GenreIds { get; set; }
    }
}