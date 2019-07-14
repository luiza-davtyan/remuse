using BookService.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RemuseWebApplication.HttpServices
{
    public class BookServices
    {
        HttpClient httpClient = new HttpClient();

        public BookServices()
        {
           
        }

        public async Task<List<Book>> GetAllBooks()
        {
          var response = await  httpClient.GetAsync("http://localhost:51858/api/book");
          var books =  JsonConvert.DeserializeObject<IEnumerable<Book>>(await response.Content.ReadAsStringAsync());
          return books.ToList();
        }

        public async Task CreateNewBooks(Book book)
        {
            var jsonObject = JsonConvert.SerializeObject(book);
            var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("http://localhost:51858/api/book", content);
        }
    }
}
