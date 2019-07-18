using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;
using ProfileAPI.Entities;
using ProfileAPI.Services;

namespace ProfileAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        HttpClient httpClient = new HttpClient();

        private IProfileRepository profileRepository;

        public ProfileController(IProfileRepository profileRepository)
        {
            this.profileRepository = profileRepository;
        }

        // GET profile by id
        [HttpGet("{id}")]
        public async Task<ActionResult<List<BookDTO>>> Get(int userId)
        {
            //var profiles = this.profileRepository.GetUserBooks(userId);
            var books = new List<BookDTO>();
            IEnumerable<String> booksIds = (IEnumerable<String>) this.profileRepository.GetUserBooks(userId);
            foreach (var item in booksIds)
            {
                var response = await httpClient.GetAsync("http://localhost:53085/api/book/" + item);
                var book = JsonConvert.DeserializeObject<IEnumerable<BookDTO>>(await response.Content.ReadAsStringAsync());
                books.Add((BookDTO)book);
            }

            return books;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] Profile profile)
        {
            this.profileRepository.Create(profile);
        }

        // PUT api/values/5
        [HttpPut]
        public void Put([FromBody] Profile profile)
        {
            this.profileRepository.Update(profile);
        }

        // DELETE api/values/5
        [HttpDelete]
        public void Delete(Profile profile)
        {
            this.profileRepository.Delete(profile);
        }
    }
}
