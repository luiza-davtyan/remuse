using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Widget;
using Newtonsoft.Json;

namespace Remuse.Activities
{
    [Activity(Label = "BookPage")]
    public class BookPage : ListActivity
    {
        List<Book> usersbooks = GetBooks();   //get this from BooksService or activity...
        LogOutBroadcastReceiver _logOutBroadcastReceiver = new LogOutBroadcastReceiver();
        List<string> mLeftItems = new List<string>();

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

            #region menu
            
            #endregion

            ListView.TextFilterEnabled = true;

            ListView.ItemClick += delegate (object sender, AdapterView.ItemClickEventArgs args)
            {
                Intent intent = new Intent(this, typeof(OneBookInfo));
                intent.PutExtra("book", JsonConvert.SerializeObject(usersbooks[args.Position]));
                StartActivity(intent);
            };

            var intentFilter = new IntentFilter();
            intentFilter.AddAction("com.mypackagename.ActionLogOut");
            RegisterReceiver(_logOutBroadcastReceiver, intentFilter);
        }

        /// <summary>
        ///  Event,that works when user clicks on the item of menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MLeftDrawer_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Type type = typeof(UserPage);

            int position = e.Position;
            switch (position)
            {
                case 0:
                    Intent intent = new Intent(this, type);
                    StartActivity(intent);
                    break;
                case 1:
                    Toast.MakeText(this, mLeftItems[e.Position], ToastLength.Long).Show();
                    break;
                case 2:
                    Toast.MakeText(this, mLeftItems[e.Position], ToastLength.Long).Show();
                    break;
                case 3:
                    var broadcastIntent = new Intent();
                    broadcastIntent.SetAction("com.mypackagename.ActionLogOut");
                    SendBroadcast(broadcastIntent);
                    Intent intent1 = new Intent(this, typeof(StartGeneral));
                    StartActivity(intent1);
                    break;
            }
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