using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        private string bookURI = "http://localhost:53085/api/book";

        public ProfileController(IProfileRepository profileRepository)
        {
            this.profileRepository = profileRepository;
        }

        /// <summary>
        /// Get user books by userId
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        //[Authorize]
        public async Task<ActionResult<List<BookDTO>>> Get(int id)
        {
            var books = new List<BookDTO>();
            IEnumerable<String> booksIds = (IEnumerable<String>)this.profileRepository.GetUserBooks(id);
            foreach (var item in booksIds)
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"{bookURI}/{item}");
                var response = await httpClient.SendAsync(request);
                var book = JsonConvert.DeserializeObject<BookDTO>(await response.Content.ReadAsStringAsync());
                books.Add(book);
            }
            return Ok(books);
        }

        //Add profile
        [HttpPost]
        public IActionResult Post([FromBody] Profile profile)
        {
            Profile currProfile = this.profileRepository.Create(profile);
            return Ok(currProfile);
        }

        //PUT profile
        [HttpPut]
        public IActionResult Put([FromBody] Profile profile)
        {
            this.profileRepository.Update(profile);
            return Ok(profile);
        }

        //DELETE profile
        [HttpDelete]
        public IActionResult Delete(Profile profile)
        {
            this.profileRepository.Delete(profile);
            return Ok();
        }
    }
}