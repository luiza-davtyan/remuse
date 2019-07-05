using BookService.Models;
using BookService.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookService.Controllers
{
    public class GenreController : ControllerBase
    {
        private readonly GenreRepository _genreRepository;

        public GenreController(GenreRepository genreService)
        {
            _genreRepository = genreService;
        }

        [HttpGet]
        public ActionResult<List<Genre>> Get() =>
            _genreRepository.Get();

        [HttpGet("{id:length(24)}", Name = "GetGenre")]
        public ActionResult<Genre> Get(string id)
        {
            var genre = _genreRepository.Get(id);

            if (genre == null)
            {
                return NotFound();
            }

            return genre;
        }

        [HttpPost]
        public ActionResult<Genre> Create(Genre genre)
        {
            _genreRepository.Create(genre);

            return CreatedAtRoute("GetGenre", new { id = genre.Id.ToString() }, genre);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Genre genreIn)
        {
            var ganre = _genreRepository.Get(id);

            if (ganre == null)
            {
                return NotFound();
            }

            _genreRepository.Update(id, genreIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var genre = _genreRepository.Get(id);

            if (genre == null)
            {
                return NotFound();
            }

            _genreRepository.Remove(genre.Id);

            return NoContent();
        }
    }
}
