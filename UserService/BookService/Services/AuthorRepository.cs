using BookService.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookService.Services
{
    public class AuthorRepository
    {
        private readonly IMongoCollection<Author> _authors;

        public AuthorRepository(IBooksDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _authors = database.GetCollection<Author>(settings.AuthorsCollectionName);
        }

        public List<Author> Get() =>
            _authors.Find(author => true).ToList();

        public Author Get(string id) =>
            _authors.Find<Author>(author => author.Id == id).FirstOrDefault();

        public Author Create(Author author)
        {
            _authors.InsertOne(author);
            return author;
        }

        public void Update(string id, Author authorIn) =>
            _authors.ReplaceOne(author => author.Id == id, authorIn);

        public void Remove(Author authorIn) =>
            _authors.DeleteOne(author => author.Id == authorIn.Id);

        public void Remove(string id) =>
            _authors.DeleteOne(author => author.Id == id);
    }
}
