using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Widget;
using Newtonsoft.Json;
using Remuse.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Remuse.Activities
{
    [Activity(Label = "BookNumberN")]
    public class OneBookInfo : Activity
    {
        Book selectedBook;
        TextView enteredbook, author, genre, year, description;
        ScrollView scroll;
        Button read;
        List<string> mLeftItems = new List<string>();
        LogOutBroadcastReceiver _logOutBroadcastReceiver = new LogOutBroadcastReceiver();

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
            enteredbook.Text = selectedBook.Title;
            author.Text = author.Text + selectedBook.Author.FirstName + " " + selectedBook.Author.LastName;
            genre.Text = genre.Text + "Novel";
            year.Text = year.Text + selectedBook.Year;
            description.Text = selectedBook.Description;

            read.Click += Read_Click;
            #region menu
            DrawerLayout mDrawerLayout;

            // Array Adaper  
            ArrayAdapter mLeftAdapter;

            mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawerLayout1);
            ListView mLeftDrawer = FindViewById<ListView>(Resource.Id.leftsideview);
            mLeftDrawer.SetBackgroundColor(Color.White);

            mLeftItems.Add("My account");
            mLeftItems.Add("Network");
            mLeftItems.Add("Settings");
            mLeftItems.Add("Log out");

            // Set ArrayAdaper with Items  
            mLeftAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, mLeftItems);
            mLeftDrawer.Adapter = mLeftAdapter;
            mLeftDrawer.ItemClick += MLeftDrawer_ItemClick;
            #endregion

            var intentFilter = new IntentFilter();
            intentFilter.AddAction("com.mypackagename.ActionLogOut");
            RegisterReceiver(_logOutBroadcastReceiver, intentFilter);
        }

        /// <summary>
        /// Event,that works when user clicks on the item of menu
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
                    UserInfo.User = null;
                    UserInfo.Token = null;
                    UserInfo.BookId = null;
                    var broadcastIntent = new Intent();
                    broadcastIntent.SetAction("com.mypackagename.ActionLogOut");
                    SendBroadcast(broadcastIntent);
                    Intent intent1 = new Intent(this, typeof(StartGeneral));
                    StartActivity(intent1);
                    break;
            }
        }

        /// <summary>
        /// Event,that works when user clicks Read button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Read_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(BookReader));
            intent.PutExtra("book", JsonConvert.SerializeObject(selectedBook));
            StartActivity(intent);
        }

        /// <summary>
        /// Destroys activiy
        /// </summary>
        protected override void OnDestroy()
        {
            base.OnDestroy();
            UnregisterReceiver(_logOutBroadcastReceiver);
        }

        async Task<Author> GetAuthorByIdAsync(string id)
        {
            var server = new AuthorClient(new System.Net.Http.HttpClient());
            Author author = await server.GetAsync(id);
            return author;
        }
    }
}