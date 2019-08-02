using Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AdminApplication.HttpServices
{
    public class UserService
    {
        HttpClient httpClient = new HttpClient();
        private string userURI;

        public UserService(string uri)
        {
            userURI = uri;
        }

        public async Task<List<UserDTO>> GetAllUsers()
        {
            var response = await httpClient.GetAsync(userURI);
            var users = JsonConvert.DeserializeObject<IEnumerable<UserDTO>>(await response.Content.ReadAsStringAsync());
            return users.ToList();
        }

        public async Task CreateNewUser(UserDTO book)
        {
            var jsonObject = JsonConvert.SerializeObject(book);
            var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(userURI, content);
        }

        public async Task<UserDTO> FindUser(int id)
        {
            var response = await httpClient.GetAsync($"{userURI}/{id}");
            var user = JsonConvert.DeserializeObject<UserDTO>(await response.Content.ReadAsStringAsync());
            return user;
        }

        public async Task UpdateBook(UserDTO bookDTO)
        {
            var jsonObject = JsonConvert.SerializeObject(bookDTO);
            var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync($"{userURI}/{bookDTO.Id}", content);
        }

        public async Task Remove(UserDTO bookDTO)
        {
            var response = await httpClient.DeleteAsync($"{userURI}/{bookDTO.Id}");
        }
    }
}
