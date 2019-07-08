using BookService.Models;
using BookService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BookService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        /// <summary>
        /// Book repository.
        /// </summary>
        private readonly BookRepository _bookRepository;

        /// <summary>
        /// Public controller.
        /// </summary>
        /// <param name="bookService"></param>
        public BookController(BookRepository bookService)
        {
            _bookRepository = bookService;
        }

        /// <summary>
        /// Get all books.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<Book>> Get() =>
            _bookRepository.Get();

        /// <summary>
        /// Get book by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:length(24)}", Name = "GetBook")]
        public ActionResult<Book> Get(string id)
        {
            var book = _bookRepository.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        /// <summary>
        /// Create book.
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public ActionResult<Book> Create(Book book)
        {
            _bookRepository.Create(book);

            return CreatedAtRoute("GetBook", new { id = book.Id.ToString() }, book);
        }

        /// <summary>
        /// Update book by id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bookIn"></param>
        /// <returns></returns>
        [HttpPut("{id:length(24)}")]
        [Authorize]
        public IActionResult Update(string id, Book bookIn)
        {
            var book = _bookRepository.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            _bookRepository.Update(id, bookIn);

            return NoContent();
        }

        /// <summary>
        /// Delete book.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:length(24)}")]
        [Authorize]
        public IActionResult Delete(string id)
        {
            var book = _bookRepository.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            _bookRepository.Remove(book.Id);

            return NoContent();
        }

        /// <summary>
        /// Search book by title.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet("search/{param}")]
        public ActionResult<List<Book>> SearchBook(string param)
        {
            var parsedParam = param.ToLower();
            var keywords = parsedParam.Split('-');
            var results = _bookRepository.GetByTitle(keywords);
            return results;
        }
    }
}
