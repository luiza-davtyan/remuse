using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BookService.Models
{
    public class Book
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [JsonProperty("Title")]
        [BsonRequired]
        public string Title { get; set; }

        public string Content { get; set; }

        public string Description { get; set; }

        public string AuthorId { get; set; }

        public int Year { get; set; }

        public virtual List<ObjectId> GenreIds { get; set; }
    }
}

