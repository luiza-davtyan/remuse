﻿using BookService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Models
{
    public class User
    {
        // prop
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
       // public string RoleId { get; protected set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public byte[] Picture { get; set; }


        public User(string name, string surname, DateTime dateOfBirth, string email,
                    string username, string password)//, string roleId = "User")
        {
            this.Name = name;
            this.Surname = surname;
            this.DateOfBirth = dateOfBirth;
            this.Email = email;
           // this.RoleId = roleId;
            this.Username = username;
            this.Password = password;
        }
    }
}
