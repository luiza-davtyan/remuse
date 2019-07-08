using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BookService.Models
{
    public class Genre
    {
        /// <summary>
        /// Id of the genre. It is required and is automatically generated.
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// Name of the genre.
        /// </summary>
        public string Name { get; set; }
    }
}
