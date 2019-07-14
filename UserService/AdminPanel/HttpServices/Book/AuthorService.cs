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
    public class AuthorService
    {
        HttpClient httpClient = new HttpClient();

        public AuthorService()
        {

        }

        public async Task<List<Author>> GetAllAuthors()
        {
            var response = await httpClient.GetAsync("http://localhost:51858/api/author");
            var authors = JsonConvert.DeserializeObject<IEnumerable<Author>>(await response.Content.ReadAsStringAsync());
            return authors.ToList();
        }

        public async Task CreateNewAuthor(Author author)
        {
            var jsonObject = JsonConvert.SerializeObject(author);
            var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("http://localhost:51858/api/author", content);
        }
    }
}
