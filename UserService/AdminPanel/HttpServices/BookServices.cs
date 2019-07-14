using Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AdminApplication.HttpServices
{
    public class BookServices
    {
        HttpClient httpClient = new HttpClient();
        private string bookURI;

        public BookServices(string uri)
        {
            bookURI = uri;
        }

        public async Task<List<BookDTO>> GetAllBooks()
        {
          var response = await  httpClient.GetAsync(bookURI);
          var books =  JsonConvert.DeserializeObject<IEnumerable<BookDTO>>(await response.Content.ReadAsStringAsync());
          return books.ToList();
        }

        public async Task CreateNewBooks(BookDTO book)
        {
            var jsonObject = JsonConvert.SerializeObject(book);
            var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(bookURI, content);
        }

        public async Task<BookDTO> FindBook(string id)
        {
            var response = await httpClient.GetAsync($"{bookURI}/{id}");
            var book = JsonConvert.DeserializeObject<BookDTO>(await response.Content.ReadAsStringAsync());
            return book;
        }

        public async Task UpdateBook(BookDTO bookDTO)
        {
            var jsonObject = JsonConvert.SerializeObject(bookDTO);
            var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync($"{bookURI}/{bookDTO.Id}", content);
        }

        public async Task Remove(BookDTO bookDTO)
        {
            var response = await httpClient.DeleteAsync($"{bookURI}/{bookDTO.Id}");
        }
    }
}
