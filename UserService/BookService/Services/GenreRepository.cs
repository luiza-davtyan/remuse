using BookService.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookService.Services
{
    public class GenreRepository
    {
        /// <summary>
        /// Collection of genres from mongodb.
        /// </summary>
        private readonly IMongoCollection<Genre> _genres;

        /// <summary>
        /// Public construvtor that initializes database settings.
        /// </summary>
        /// <param name="settings"></param>
        public GenreRepository(IBooksDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _genres = database.GetCollection<Genre>(settings.GenresCollectionName);
        }

        /// <summary>
        /// Get list of all genres.
        /// </summary>
        /// <returns></returns>
        public List<Genre> Get() =>
            _genres.Find(genre => true).ToList();

        /// <summary>
        /// Get genre by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Genre Get(string id) =>
            _genres.Find<Genre>(genre => genre.Id == id).FirstOrDefault();

        /// <summary>
        /// Create genre.
        /// </summary>
        /// <param name="genre"></param>
        /// <returns></returns>
        public Genre Create(Genre genre)
        {
            _genres.InsertOne(genre);
            return genre;
        }

        /// <summary>
        /// Update genre by id with the given object.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="genreIn"></param>
        public void Update(string id, Genre genreIn) =>
            _genres.ReplaceOne(genre => genre.Id == id, genreIn);

        /// <summary>
        /// Delete genre by object.
        /// </summary>
        /// <param name="genreIn"></param>
        public void Remove(Genre genreIn) =>
            _genres.DeleteOne(genre => genre.Id == genreIn.Id);

        /// <summary>
        /// Delete genre by id.
        /// </summary>
        /// <param name="id"></param>
        public void Remove(string id) =>
            _genres.DeleteOne(genre => genre.Id == id);
    }
}
