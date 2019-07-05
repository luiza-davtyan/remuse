using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Remuse.Models
{
    public static class User
    {
        
        public static string FirstName { get; set; }
        public static string LastName { get; set; }
        public static string Email { get; set; }
        public static string Password { get; set; }
        public static string UserName { get; set; }
        public static DateTime BirthDay { get; set; }
    }
}