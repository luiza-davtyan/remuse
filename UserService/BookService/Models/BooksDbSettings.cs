using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookService.Models
{
    public class BooksDbSettings : IBooksDbSettings
    {
        public string BooksCollectionName { get; set; }
        public string AuthorsCollectionName { get; set; }
        public string GenresCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IBooksDbSettings
    {
        string BooksCollectionName { get; set; }
        string AuthorsCollectionName { get; set; }
        string GenresCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
