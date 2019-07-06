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
    public class BookController : ControllerBase
    {
        private readonly BookRepository _bookRepository;

        public BookController(BookRepository bookService)
        {
            _bookRepository = bookService;
        }

        [HttpGet]
        public ActionResult<List<Book>> Get() =>
            _bookRepository.Get();

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

        [HttpPost]
        public ActionResult<Book> Create(Book book)
        {
            _bookRepository.Create(book);

            return CreatedAtRoute("GetBook", new { id = book.Id.ToString() }, book);
        }

        [HttpPut("{id:length(24)}")]
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

        [HttpDelete("{id:length(24)}")]
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
