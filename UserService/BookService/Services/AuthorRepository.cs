using BookService.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BookService.Services
{
    public class AuthorRepository
    {
        /// <summary>
        /// Collection of authors from mongodb.
        /// </summary>
        private readonly IMongoCollection<Author> _authors;

        /// <summary>
        /// Pulbic constructor that initializes db settings.
        /// </summary>
        /// <param name="settings"></param>
        public AuthorRepository(IBooksDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _authors = database.GetCollection<Author>(settings.AuthorsCollectionName);
        }

        /// <summary>
        /// Get list of all authors.
        /// </summary>
        /// <returns></returns>
        public List<Author> Get() =>
            _authors.Find(author => true).ToList();

        /// <summary>
        /// Get author by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Author Get(string id) =>
            _authors.Find<Author>(author => author.Id == id).FirstOrDefault();

        /// <summary>
        /// Create an author.
        /// </summary>
        /// <param name="author"></param>
        /// <returns></returns>
        public Author Create(Author author)
        {
            _authors.InsertOne(author);
            return author;
        }

        /// <summary>
        /// Update an author by id with the given object,
        /// </summary>
        /// <param name="id"></param>
        /// <param name="authorIn"></param>
        public void Update(string id, Author authorIn) =>
            _authors.ReplaceOne(author => author.Id == id, authorIn);

        /// <summary>
        /// Delete author by object.
        /// </summary>
        /// <param name="authorIn"></param>
        public void Remove(Author authorIn) =>
            _authors.DeleteOne(author => author.Id == authorIn.Id);

        /// <summary>
        /// Delele object by id.
        /// </summary>
        /// <param name="id"></param>
        public void Remove(string id) =>
            _authors.DeleteOne(author => author.Id == id);
    }
}
