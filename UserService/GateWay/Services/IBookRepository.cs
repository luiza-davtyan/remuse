using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GateWay.Models;

namespace GateWay.Services
{
    public interface IBookRepository
    {
        List<BookDTO> GetBooks();
        BookDTO GetBookById(int id);
        void AddBook(BookDTO book);
    }
}
