using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GateWay.Models;

namespace GateWay.Services
{
    public class BookRepository : IBookRepository
    {
        public  List<BookDTO> books;

        public BookRepository(List<BookDTO> books)
        {
            this.books = books;
        }

        public void AddBook(BookDTO book)
        {
            books.Add(book);
        }

        public BookDTO GetBookById(int id)
        {
            return books.Where(x => x.Id == id).FirstOrDefault();
        }

        public List<BookDTO> GetBooks()
        {
            return books;
        }
    }
}
