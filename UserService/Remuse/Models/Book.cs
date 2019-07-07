namespace Remuse.Models
{
    public class Book
    {
       // [BsonId]
       // [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        //[JsonProperty("Title")]
        //[BsonRequired]
        public string Title { get; set; }

        public string Content { get; set; }

        public string Description { get; set; }

        public int AuthorId { get; set; }

        public string Author { get; set; }

        public int Year { get; set; }

        public string Path { get; set; }

        public string Genre { get; set; }
    }
}