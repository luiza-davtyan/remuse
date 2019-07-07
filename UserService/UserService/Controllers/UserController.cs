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
            if(userById == null)
            {
                return NotFound();
            }
            return Ok(userById);
        }

        [HttpPost]
        public ActionResult<User> AddUser(User user)
        {
            User currUser = this.userRepository.GetUserByUsername(user.Username);
            if (currUser != null)
            {
                throw new Exception("Username must be unique");//??
            }
            user.Password = getHashSha256(user.Password);
            this.userRepository.AddUser(user);
            return CreatedAtRoute("GetUser", new { id = user.Id.ToString() }, user);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = this.userRepository.GetUserByID(id);
            if (user == null)
            {
                return NotFound();
            }

            this.userRepository.DeleteUserById(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        IActionResult Update(User newUser)
        {
            var user = this.userRepository.GetUserByID(newUser.Id);
            if (user == null)
            {
                return NotFound();
            }

            newUser.Password = getHashSha256(newUser.Password);
            this.userRepository.Update(user);
            return NoContent();
        }

        public string getHashSha256(string text)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(text);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }

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
    }
}
