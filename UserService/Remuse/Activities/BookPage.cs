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
using Remuse.Models;

namespace Remuse.Activities
{
    [Activity(Label = "BookPage")]
    public class BookPage : ListActivity
    {
        List<Book> usersbooks = GetBooks();   //get this from BooksService or activity...

        //static readonly string[] countries = new String[]{"resresr","rse","fsf","fesfse","fsefesf","fesfesf","fesfesfes","fsefes","hgydyg","rssgr","gdsgrdg0","fshyfsf","fesfes","awdwd","dwadwad","#224","543543","43242","75764","bareeeeev"};
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
                new Book() { Id = "1", Title = "Title 1" },
                new Book() { Id = "2", Title = "Title 2" },
                new Book() { Id = "3", Title = "Title 3" },
                new Book() { Id = "4", Title = "Title 4" }
            };
            //to do
            return books;
        }
    }
}