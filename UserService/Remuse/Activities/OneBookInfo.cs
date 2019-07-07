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
using Xamarin.Android;
namespace Remuse.Activities
{
    [Activity(Label = "BookNumberN")]
    public class OneBookInfo : Activity
    {
        Book selectedBook;
        TextView enteredbook,author,genre,year,description;
        ScrollView scroll;
        Button read;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.bookinfo);

            selectedBook = JsonConvert.DeserializeObject<Book>(Intent.GetStringExtra("book"));

            //Creating TextViews on the page
            enteredbook = FindViewById<TextView>(Resource.Id.textView1);
            author = FindViewById<TextView>(Resource.Id.textView2);
            genre = FindViewById<TextView>(Resource.Id.textView3);
            year = FindViewById<TextView>(Resource.Id.textView4);
            scroll = FindViewById<ScrollView>(Resource.Id.scrollView1);
            description = FindViewById<TextView>(Resource.Id.textView6);
            read = FindViewById<Button>(Resource.Id.button1);

            //Giving info to TextViews
            enteredbook.Text = selectedBook.Title + (string)selectedBook.Id;
            author.Text = author.Text + selectedBook.Author;
            genre.Text = genre.Text + selectedBook.Genre;
            year.Text = year.Text + selectedBook.Year;
            description.Text = selectedBook.Description;

            read.Click += Read_Click;
        }

        private void Read_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this,typeof(BookReader));
            StartActivity(intent);
        }
    }
}