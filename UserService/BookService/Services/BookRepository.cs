using BookService.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookService.Services
{
    public class BookRepository
    {
        private readonly IMongoCollection<Book> _books;

        public BookRepository(IBooksDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _books = database.GetCollection<Book>(settings.BooksCollectionName);
        }

        public List<Book> Get() =>
            _books.Find(book => true).ToList();

        public Book Get(string id) =>
            _books.Find<Book>(book => book.Id == id).FirstOrDefault();

        public Book Create(Book book)
        {
            _books.InsertOne(book);
            return book;
        }

        public void Update(string id, Book bookIn) =>
            _books.ReplaceOne(book => book.Id == id, bookIn);

        public void Remove(Book bookIn) =>
            _books.DeleteOne(book => book.Id == bookIn.Id);

        public void Remove(string id) =>
            _books.DeleteOne(book => book.Id == id);

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
