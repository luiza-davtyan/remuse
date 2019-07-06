using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserService.Models;
using UserService.Services;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserRepository userRepository;
        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = this.userRepository.GetUsers();
            return Ok(users);
        }
 
        [Route("{id}")]
        [HttpGet]
        public IActionResult GetUserById(int id)
        {
            var userById = this.userRepository.GetUserByID(id);
            //var bookDTO = Mapper.Map<BookDTO>(bookById);
            return Ok(userById);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            //var user = UserRepository..Where(x => x.Id == int.Parse(id)).FirstOrDefault();
            this.userRepository.DeleteUserById(id);
        }

        [HttpPut("{id}")]
        void Update(User newUser)
        {
            this.userRepository.Update(newUser);
        }

        [HttpPost]
        void AddUser(string firstName, string lastName, DateTime dateOfBirth, string email,
                    string username, string password)
        {
           // password = getHashSha256(password);
            this.userRepository.AddUser(firstName, lastName, dateOfBirth, email, username, password);
        }

        [HttpPut("{id}")]
        public void ChangePassword(string newPassword)
        {

        }

        //public string getHashSha256(string text)
        //{
        //    byte[] bytes = Encoding.Unicode.GetBytes(text);
        //    SHA256Managed hashstring = new SHA256Managed();
        //    byte[] hash = hashstring.ComputeHash(bytes);
        //    string hashString = string.Empty;
        //    foreach (byte x in hash)
        //    {
        //        hashString += String.Format("{0:x2}", x);
        //    }
        //    return hashString;
        //}

        //public bool PasswordIsValid(string password)
        //{
        //    var input = password;

        //    if (string.IsNullOrWhiteSpace(input))
        //    {
        //        throw new Exception("Password should not be empty");
        //    }

        //    var hasNumber = new Regex(@"[0-9]+");
        //    var hasUpperChar = new Regex(@"[A-Z]+");
        //    var hasMiniMaxChars = new Regex(@".{8,15}");
        //    var hasLowerChar = new Regex(@"[a-z]+");
        //    var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

        //    if (!hasLowerChar.IsMatch(input))
        //    {
        //        throw new Exception("Password should contain At least one lower case letter");
        //    }
        //    else if (!hasUpperChar.IsMatch(input))
        //    {
        //        throw new Exception("Password should contain At least one upper case letter");
        //    }
        //    else if (!hasMiniMaxChars.IsMatch(input))
        //    {
        //        throw new Exception("Password should not be less than 8 or greater than 12 characters");
        //    }
        //    else if (!hasNumber.IsMatch(input))
        //    {
        //        throw new Exception("Password should contain At least one numeric value");
        //    }
        //    else if (!hasSymbols.IsMatch(input))
        //    {
        //        throw new Exception("Password should contain At least one special case characters");
        //    }

        //    return true;
        //}

        //// GET api/values
        //[HttpGet]
        //public ActionResult<IEnumerable<string>> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/values/5
        //[HttpGet("{id}")]
        //public ActionResult<string> Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}
    }
}
