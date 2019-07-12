using BookService.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BookService.Services
{
    public class BookRepository
    {
        /// <summary>
        /// Collection of books from mongodb.
        /// </summary>
        private readonly IMongoCollection<Book> _books;

        /// <summary>
        /// Public construvtor that initializes database settings.
        /// </summary>
        /// <param name="settings"></param>
        public BookRepository(IBooksDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _books = database.GetCollection<Book>(settings.BooksCollectionName);
        }

        /// <summary>
        /// Get list of all books.
        /// </summary>
        /// <returns></returns>
        public List<Book> Get() =>
            _books.Find(book => true).ToList();

        /// <summary>
        /// Get book by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Book Get(string id) =>
            _books.Find<Book>(book => book.Id == id).FirstOrDefault();

        /// <summary>
        /// Create book.
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public Book Create(Book book)
        {
            _books.InsertOne(book);
            return book;
        }

        /// <summary>
        /// Update book by id with the given object.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bookIn"></param>
        public void Update(string id, Book bookIn) =>
            _books.ReplaceOne(book => book.Id == id, bookIn);

        /// <summary>
        /// Delete Book by object.
        /// </summary>
        /// <param name="bookIn"></param>
        public void Remove(Book bookIn) =>
            _books.DeleteOne(book => book.Id == bookIn.Id);

        /// <summary>
        /// Delete book by id.
        /// </summary>
        /// <param name="id"></param>
        public void Remove(string id) =>
            _books.DeleteOne(book => book.Id == id);

        /// <summary>
        /// Search by title. Returns list of books, which contain the given keyword(keywords) in title.
        /// </summary>
        /// <param name="keywords"></param>
        /// <returns></returns>
        public List<Book> GetByTitle(string[] keywords)
        {
            var resultsList = new List<Book>();
            foreach (var item in keywords)
            {
                resultsList.AddRange(_books.Find<Book>(book => book.Title.ToLower().Contains(item)).ToList());
            }
            return resultsList;
        }
    }
}
