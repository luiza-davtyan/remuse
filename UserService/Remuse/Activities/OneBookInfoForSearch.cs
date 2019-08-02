using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Widget;
using Newtonsoft.Json;
using ProfileNameSpaceOurClient;
using Remuse.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Remuse.Activities
{
    [Activity(Label = "OneBookInfoForSearch")]
    public class OneBookInfoForSearch : Activity
    {
        Book selectedBook;
        TextView enteredbook, author, genre, year, description;
        ScrollView scroll;
        Button read, add;
        List<string> mLeftItems = new List<string>();
        LogOutBroadcastReceiver _logOutBroadcastReceiver = new LogOutBroadcastReceiver();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.onebookinfo_forsearch);

            selectedBook = JsonConvert.DeserializeObject<Book>(Intent.GetStringExtra("book"));

            //Creating TextViews on the page
            #region Textviews and buttons
            enteredbook = FindViewById<TextView>(Resource.Id.textView1);
            author = FindViewById<TextView>(Resource.Id.textView2);
            genre = FindViewById<TextView>(Resource.Id.textView3);
            year = FindViewById<TextView>(Resource.Id.textView4);
            scroll = FindViewById<ScrollView>(Resource.Id.scrollView1);
            description = FindViewById<TextView>(Resource.Id.textView6);
            read = FindViewById<Button>(Resource.Id.button1);
            add = FindViewById<Button>(Resource.Id.button2);
            #endregion

            //Giving info to TextViews
            enteredbook.Text = selectedBook.Title;
            author.Text = author.Text + selectedBook.Author.FirstName + " " + selectedBook.Author.LastName;
            genre.Text = genre.Text + "Novel";
            year.Text = year.Text + selectedBook.Year;
            description.Text = selectedBook.Description;

            read.Click += Read_Click;
            add.Click += Add_Click;

            #region Menu
            DrawerLayout mDrawerLayout;

            // Array Adaper  
            ArrayAdapter mLeftAdapter;

            mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawerLayout1);
            ListView mLeftDrawer = FindViewById<ListView>(Resource.Id.leftsideview);
            mLeftDrawer.SetBackgroundColor(Color.White);

            if (UserInfo.User != null)
            {
                mLeftItems.Add("My account");
            }
            mLeftItems.Add("Home page");
            mLeftItems.Add("Network");
            mLeftItems.Add("Settings");
            if (UserInfo.User != null)
            {
                mLeftItems.Add("Log out");
            }

            // Set ArrayAdaper with Items  
            mLeftAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, mLeftItems);
            mLeftDrawer.Adapter = mLeftAdapter;
            mLeftDrawer.ItemClick += MLeftDrawer_ItemClick;
            #endregion

            var intentFilter = new IntentFilter();
            intentFilter.AddAction("com.mypackagename.ActionLogOut");
            RegisterReceiver(_logOutBroadcastReceiver, intentFilter);
        }

        /// <summary>..
        /// Event,when user clicks ADD button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Add_Click(object sender, EventArgs e)
        {
            if (UserInfo.Token == null)
            {
                Toast.MakeText(this, "Please,log in to add books in your list", ToastLength.Long).Show();
                return;
            }
            string bookId = selectedBook.Id;
            ProfileNameSpaceOurClient.Profile profile = new ProfileNameSpaceOurClient.Profile();

            profile.UserId = UserInfo.User.Id;
            profile.BookId = selectedBook.Id;
            Models.Profile profile1 = new Models.Profile(profile.Id, profile.BookId);

            if (UserInfo.Books.Count == 0)
            {
                var server = new ProfileClient(new HttpClient());
                await server.PostAsync(profile);
                UserInfo.profiles.Add(profile1);
                UserInfo.BookId.Add(selectedBook.Id);
                UserInfo.Books.Add(selectedBook);

                Toast.MakeText(this, "The book was added to your list", ToastLength.Long).Show();
            }
            else
            {
                for (int i = 0; i < UserInfo.Books.Count; i++)
                {
                    UserInfo.BookId.Add(UserInfo.Books[i].Id);
                }

                foreach (var item in UserInfo.BookId)
                {
                    if (item == bookId)
                    {
                        Toast.MakeText(this, selectedBook.Title + " already exists in your list", ToastLength.Long).Show();
                        return;
                    }
                }

                var server = new ProfileClient(new HttpClient());
                await server.PostAsync(profile);
                UserInfo.BookId.Add(selectedBook.Id);
                UserInfo.Books.Add(selectedBook);
                UserInfo.profiles.Add(profile1);
                Toast.MakeText(this, "The book was added to your list", ToastLength.Short).Show();
            }
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
            if (UserInfo.User == null)
            {
                switch (position)
                {
                    case 0:
                        type = typeof(MainActivity);
                        break;
                    case 1:
                        Toast.MakeText(this, mLeftItems[e.Position], ToastLength.Long).Show();
                        return;
                    case 2:
                        Toast.MakeText(this, mLeftItems[e.Position], ToastLength.Long).Show();
                        return;
                }
                Intent intent0 = new Intent(this, type);
                StartActivity(intent0);
            }
            else
            {
                switch (position)
                {
                    case 0:
                        type = typeof(UserPage);
                        break;
                    case 1:
                        type = typeof(General);
                        break;
                    case 2:
                        Toast.MakeText(this, mLeftItems[e.Position], ToastLength.Long).Show();
                        return;
                    case 3:
                        type = typeof(SettingsPage);
                        break;
                    case 4:
                        UserInfo.User = null;
                        UserInfo.Token = null;
                        UserInfo.BookId = null;
                        var broadcastIntent = new Intent();
                        broadcastIntent.SetAction("com.mypackagename.ActionLogOut");
                        SendBroadcast(broadcastIntent);
                        type = typeof(StartGeneral);
                        break;
                }
                Intent intent = new Intent(this, type);
                StartActivity(intent);
            }
        }

        /// <summary>
        /// Event,that works when user clicks Read button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Read_Click(object sender, EventArgs e)
        {
            if (UserInfo.Token == null)
            {
                Intent intent1 = new Intent(this, typeof(BookReaderForGuests));
                intent1.PutExtra("book", JsonConvert.SerializeObject(selectedBook));
                StartActivity(intent1);
            }
            else
            {
                Intent intent = new Intent(this, typeof(BookReader));
                intent.PutExtra("book", JsonConvert.SerializeObject(selectedBook));
                StartActivity(intent);
            }
        }

        /// <summary>
        /// Destroys activiy
        /// </summary>
        protected override void OnDestroy()
        {
            base.OnDestroy();
            UnregisterReceiver(_logOutBroadcastReceiver);
        }

        /// <summary>
        /// Gets author by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        async Task<Author> GetAuthorByIdAsync(string id)
        {
            var server = new AuthorClient(new System.Net.Http.HttpClient());
            Author author = await server.GetAsync(id);
            return author;
        }
    }
}