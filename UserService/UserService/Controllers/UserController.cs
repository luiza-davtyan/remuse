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
        /// <summary>
        /// User repository.
        /// </summary>
        private readonly IUserRepository userRepository;
        /// <summary>
        /// A helper class object.
        /// </summary>
        private readonly Helper helper;

        /// <summary>
        /// Public user controller.
        /// </summary>
        /// <param name="userRepository"></param>
        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = this.userRepository.GetUsers();
            return Ok(users);
        }
 
        /// <summary>
        /// Get user by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Add user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<User> AddUser(User user)
        {
            User currUser = this.userRepository.GetUserByUsername(user.Username);
            if (currUser != null)
            {
                throw new Exception("Username must be unique");
            }
            user.Password = helper.getHashSha256(user.Password);
            this.userRepository.AddUser(user);
            return Ok(user);//CreatedAtRoute("GetUser", new { id = user.Id.ToString() }, user);
        }

        /// <summary>
        /// Delete book by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
            return Ok();//NoContent();
        }

        /// <summary>
        /// Update user by Id with the given object.
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        IActionResult Update(User newUser)
        {
            var user = this.userRepository.GetUserByID(newUser.Id);
            if (user == null)
            {
                return NotFound();
            }

            newUser.Password = helper.getHashSha256(newUser.Password);
            this.userRepository.Update(user);
            return Ok(user);//NoContent();
        }
    }
}
