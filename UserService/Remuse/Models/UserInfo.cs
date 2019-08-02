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
using MyNamespace;

namespace Remuse.Models
{
    public static class UserInfo
    {
        public static User User { get; set; }
        public static List<string> BookId = new List<string>();
        public static List<Book> Books = new List<Book>();
        public static AuthServerResponse Token { get; set; }
        public static List<Profile> profiles = new List<Profile>();
    }
}