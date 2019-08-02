using BookService.Models;
using BookService.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        /// <summary>
        /// Author repository.
        /// </summary>
        private readonly AuthorRepository _authorRepository;

        /// <summary>
        /// Public controller.
        /// </summary>
        /// <param name="authorService"></param>
        public AuthorController(AuthorRepository authorService)
        {
            _authorRepository = authorService;
        }

        /// <summary>
        /// Get all authors.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<Author>> Get() =>
            _authorRepository.Get();

        /// <summary>
        /// Get author by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:length(24)}", Name = "GetAuthor")]
        public ActionResult<Author> Get(string id)
        {
            var author = _authorRepository.Get(id);

            if (author == null)
            {
                return NotFound();
            }

            return author;
        }

        /// <summary>
        /// Create author.
        /// </summary>
        /// <param name="author"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<Author> Create(Author author)
        {
            _authorRepository.Create(author);

            return CreatedAtRoute("GetAuthor", new { id = author.Id.ToString() }, author);
        }

        /// <summary>
        /// Update author.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="authorIn"></param>
        /// <returns></returns>
        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Author authorIn)
        {
            var author = _authorRepository.Get(id);

            if (author == null)
            {
                return NotFound();
            }

            _authorRepository.Update(id, authorIn);

            return NoContent();
        }

        /// <summary>
        /// Delete author.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var author = _authorRepository.Get(id);

            if (author == null)
            {
                return NotFound();
            }

            _authorRepository.Remove(author.Id);

            return NoContent();
        }
    }
}
