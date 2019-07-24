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
    public class Profile
    {
        public int ID { get; set; }
        public int UserId { get; set; }
        public string BookId { get; set; }

        public Profile(int userId, string bookId)
        {
            this.BookId = bookId;
            this.UserId = userId;
        }
    }
}