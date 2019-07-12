using BookService.Models;
using BookService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BookService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        /// <summary>
        /// Genre repository.
        /// </summary>
        private readonly GenreRepository _genreRepository;

        /// <summary>
        /// Public controller.
        /// </summary>
        /// <param name="genreService"></param>
        public GenreController(GenreRepository genreService)
        {
            _genreRepository = genreService;
        }

        /// <summary>
        /// Get list of all genres.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<Genre>> Get() =>
            _genreRepository.Get();

        /// <summary>
        /// Get genre by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Create genre.
        /// </summary>
        /// <param name="genre"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public ActionResult<Genre> Create(Genre genre)
        {
            _genreRepository.Create(genre);

            return CreatedAtRoute("GetGenre", new { id = genre.Id.ToString() }, genre);
        }

        /// <summary>
        /// Update genre.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="genreIn"></param>
        /// <returns></returns>
        [HttpPut("{id:length(24)}")]
        [Authorize]
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

        /// <summary>
        /// Delete genre.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:length(24)}")]
        [Authorize]
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
