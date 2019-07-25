using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AdminApplication.HttpServices
{
    public class ProfileService
    {
        private HttpClient httpClient = new HttpClient();
        private string profileURI;

        public ProfileService(string uri)
        {
            this.profileURI = uri;
        }

        public async Task<IEnumerable<BookDTO>> GetUserBooks(int userId)
        {
            var response = await httpClient.GetAsync($"{profileURI}/{userId}");
            var books = JsonConvert.DeserializeObject<IEnumerable<BookDTO>>(await response.Content.ReadAsStringAsync());
            return books.ToList();
        }

        public async Task Delete(ProfileDTO profile)
        {
            await httpClient.DeleteAsync($"{profileURI}/{profile.UserId}/{profile.BookId}");
        }
    }
}
