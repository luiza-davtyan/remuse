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
    [Activity(Label = "BookSearchResult")]
    public class BookSearchResult : ListActivity
    {
        List<Book> searchBooksResult = new List<Book>();  //get this from BooksService or activity...

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

            ListView.ItemClick += delegate (object sender, AdapterView.ItemClickEventArgs args)
            {
                Intent intent = new Intent(this, typeof(OneBookInfo));
                intent.PutExtra("book", JsonConvert.SerializeObject(searchBooksResult[args.Position]));
                StartActivity(intent);
            };
        }
    }
}