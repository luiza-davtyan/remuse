﻿using System;
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

namespace Remuse.Activities
{
    [Activity(Label = "BookSearchResult")]
    public class BookSearchResult : ListActivity
    {
        List<Book> searchBooksResult = new List<Book>();  //get this from BooksService or activity...
        Author authorFromBase;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            searchBooksResult = JsonConvert.DeserializeObject<List<Book>>(Intent.GetStringExtra("book"));
            List<string> titles = new List<string>();

            foreach (Book book in searchBooksResult)
            {
                titles.Add(book.Title);
            }
            ListAdapter = new ArrayAdapter<string>(this, Resource.Layout.list_item, titles);

            ListView.TextFilterEnabled = true;

            ListView.ItemClick += async delegate (object sender, AdapterView.ItemClickEventArgs args)
            {
                var server = new AuthorClient(new System.Net.Http.HttpClient());
                authorFromBase = await server.GetAsync(searchBooksResult[args.Position].AuthorId);
                searchBooksResult[args.Position].Author = authorFromBase;

                Intent intent = new Intent(this, typeof(OneBookInfo));
                intent.PutExtra("book", JsonConvert.SerializeObject(searchBooksResult[args.Position]));
                StartActivity(intent);
            };
        }
    }
}