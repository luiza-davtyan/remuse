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
        private string bookURI = "http://localhost:51654/api/book";

        public ProfileController(IProfileRepository profileRepository)
        {
            this.profileRepository = profileRepository;
        }

        // GET profile by id
        //[Route]
        [HttpGet("{id}")]
        public async Task<ActionResult<List<BookDTO>>> Get(int id)
        {
            //var profiles = this.profileRepository.GetUserBooks(userId);
            var books = new List<BookDTO>();
            IEnumerable<String> booksIds = (IEnumerable<String>)this.profileRepository.GetUserBooks(id);
            foreach (var item in booksIds)
            {
                //string str = "http://localhost:51654/api/book/5d36a917e878205980023817";
                string str1 = $"{bookURI}/{item}";
                // var response = await httpClient.GetAsync($"{bookURI}/{item}");
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, str1);
                //var book = JsonConvert.DeserializeObject<IEnumerable<BookDTO>>(await response.Content.ReadAsStringAsync());
                var response = await httpClient.SendAsync(request);
                var book = JsonConvert.DeserializeObject<BookDTO>(await response.Content.ReadAsStringAsync());
                books.Add(book);
            }

            return Ok(books);
        }



        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] Profile profile)
        {
            Profile currProfile = this.profileRepository.Create(profile);
            return Ok(currProfile);
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
