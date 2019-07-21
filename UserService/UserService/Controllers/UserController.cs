using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
        private readonly Hasher helper;

        private List<User> users = new List<User>() { new User("AA","S", "","" ,"") };

        /// <summary>
        /// Public user controller wich initialize .
        /// </summary>
        /// <param name="userRepository"></param>
        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
            this.helper = new Hasher();
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
            //return Ok(users.First());

            var userById = this.userRepository.GetUserByID(id);
            if (userById == null)
            {
                return NotFound();
            }
            return Ok(userById);
        }

        /// <summary>
        /// Get user by Username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get/{username}")]
        public IActionResult GetUserByUsername(string username)
        {
            User currUser = this.userRepository.GetUserByUsername(username);

            if (currUser == null)
            {
                return NotFound();
            }
            return Ok(currUser);
        }


        //[Route("{username}")]
        //[HttpGet("get/{param}")]
        //public IActionResult GetUserByUsername(User user)
        //{
        //    User currUser = this.userRepository.GetUserByUsername(user.Username);
        //    if (currUser == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(currUser);
        //}

        //[HttpGet("username/{username}")]
        //public IActionResult GetUserByUsername(User user)
        //{
        //    User curruser = this.userRepository.GetUserByUsername(user.Username);
        //    if (curruser == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(curruser);
        //}

        /// <summary>
        /// Add user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<User> AddUser(User user)
        {
            User currUser = this.userRepository.GetUserByUsername(user.Username);
            User currUserEmail = this.userRepository.GetUserByEmail(user.Email);

            if (currUser != null || currUserEmail != null)
            {
                return NoContent();
            }
            user.Password = helper.GetHashSha256(user.Password);
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
        //[Authorize]
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
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        //[Authorize]
        public IActionResult Update(User user)
        {
            var updateUser = this.userRepository.GetUserByID(user.Id);

            if (updateUser == null) 
            {
                return NotFound();
            }

            user.Password = helper.GetHashSha256(user.Password);
            this.userRepository.Update(user);
            return Ok(user);
        }

        /// <summary>
        /// Changes user photo
        /// </summary>
        /// <param name="pic"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPut("photo/{userId}")]
        [Authorize]
        public IActionResult ChangePhoto([FromBody]byte[] pic, int userId)
        {
            var user = this.userRepository.GetUserByID(userId);
            if (user == null)
            {
                return NotFound();
            }

            this.userRepository.ChangePicture(pic, userId);
            return Ok(user);
        }
    }
}
