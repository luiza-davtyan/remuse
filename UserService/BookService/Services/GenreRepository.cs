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
        private readonly IMongoCollection<Genre> _genres;

        public GenreRepository(IBooksDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _genres = database.GetCollection<Genre>(settings.GenresCollectionName);
        }

        public List<Genre> Get() =>
            _genres.Find(genre => true).ToList();

        public Genre Get(string id) =>
            _genres.Find<Genre>(genre => genre.Id == id).FirstOrDefault();

        public Genre Create(Genre genre)
        {
            _genres.InsertOne(genre);
            return genre;
        }

        public void Update(string id, Genre genreIn) =>
            _genres.ReplaceOne(genre => genre.Id == id, genreIn);

        public void Remove(Genre genreIn) =>
            _genres.DeleteOne(genre => genre.Id == genreIn.Id);

        public void Remove(string id) =>
            _genres.DeleteOne(genre => genre.Id == id);
    }
}
