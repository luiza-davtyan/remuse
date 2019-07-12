﻿using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Newtonsoft.Json;
using Remuse.Models;

namespace Remuse.Activities
{
    [Activity(Label = "BookPage")]
    public class BookPage : ListActivity
    {
        List<Book> usersbooks = GetBooks();   //get this from BooksService or activity...

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //to do ...
            List<string> titles = new List<string>();
            foreach (Book book in usersbooks)
            {
                titles.Add(book.Title);
            }
            ListAdapter = new ArrayAdapter<string>(this, Resource.Layout.list_item, titles);

            ListView.TextFilterEnabled = true;

            ListView.ItemClick += delegate (object sender, AdapterView.ItemClickEventArgs args)
            {
                Intent intent = new Intent(this, typeof(OneBookInfo));
                intent.PutExtra("book", JsonConvert.SerializeObject(usersbooks[args.Position]));
                StartActivity(intent);
            };
        }

        /// <summary>
        /// Gets data from BooksService
        /// </summary>
        /// <returns></returns>
        public static List<Book> GetBooks()
        {
            List<Book> books = new List<Book>()
            {
                new Book() { Id = "1", Title = "Title 1 ",Year = 1995,Description = Desc() },
                new Book() { Id = "2", Title = "Title 2 ",Year = 1950,Description = Desc()},
                new Book() { Id = "3", Title = "Title 3 ",Year = 1940,Description = Desc()},
                new Book() { Id = "4", Title = "Title 4 " ,Year = 1352,Description = Desc()}
            };
            //Connect with bookService to get user's books
            //To do...
            return books;
        }

        /// <summary>
        /// Method ,that gets Book's description
        /// </summary>
        /// <returns></returns>
        public static string Desc()
        {
            string description = "Aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            for (int i = 0; i < 1000; i++)
            {
                description = description + "e";
            }
            return description;
        }
    }
}