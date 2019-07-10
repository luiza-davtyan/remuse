
namespace BookService.Models
{
    public class Genre
    {
        /// <summary>
        /// Id of the genre. It is required and is automatically generated.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Name of the genre.
        /// </summary>
        public string Name { get; set; }
    }
}
