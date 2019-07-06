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
using Newtonsoft.Json;
using Remuse.Models;

namespace Remuse.Activities
{
    [Activity(Label = "BookNumberN")]
    public class OneBookInfo : Activity
    {
        Book selectedBook;
        TextView enteredbook;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.bookinfo);
            selectedBook = JsonConvert.DeserializeObject<Book>(Intent.GetStringExtra("book"));
            enteredbook = FindViewById<TextView>(Resource.Id.textView1);
            enteredbook.Text = selectedBook.Title + (string)selectedBook.Id;
        }
    }
}