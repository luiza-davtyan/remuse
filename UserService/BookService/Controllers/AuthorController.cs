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
        private readonly AuthorRepository _authorRepository;

        public AuthorController(AuthorRepository authorService)
        {
            _authorRepository = authorService;
        }

        [HttpGet]
        public ActionResult<List<Author>> Get() =>
            _authorRepository.Get();

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

        [HttpPost]
        public ActionResult<Author> Create(Author author)
        {
            _authorRepository.Create(author);

            return CreatedAtRoute("GetAuthor", new { id = author.Id.ToString() }, author);
        }

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
