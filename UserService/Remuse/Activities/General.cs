using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Widget;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MyNamespace;
using System.Net.Http;
using System.Net.Http.Headers;
using Remuse.Models;

namespace Remuse.Activities
{
    [Activity(Label = "General")]
    public class General : Activity
    {
        User user;
        Button search;
        ImageView[] bookImages = new ImageView[8];
        TextView[] textViews = new TextView[8];
        List<string> mLeftItems = new List<string>();
        AutoCompleteTextView userInput;
        List<Book> books = new List<Book>();
        LogOutBroadcastReceiver _logOutBroadcastReceiver = new LogOutBroadcastReceiver();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.startgeneral);
            //user = JsonConvert.DeserializeObject<User>(Intent.GetStringExtra("user"));
            user = UserInfo.User;
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

            #region Page's images...
            bookImages[0] = FindViewById<ImageView>(Resource.Id.imageView1);
            bookImages[1] = FindViewById<ImageView>(Resource.Id.imageView2);
            bookImages[2] = FindViewById<ImageView>(Resource.Id.imageView3);
            bookImages[3] = FindViewById<ImageView>(Resource.Id.imageView4);
            bookImages[4] = FindViewById<ImageView>(Resource.Id.imageView5);
            bookImages[5] = FindViewById<ImageView>(Resource.Id.imageView6);
            bookImages[6] = FindViewById<ImageView>(Resource.Id.imageView7);
            bookImages[7] = FindViewById<ImageView>(Resource.Id.imageView8);

            textViews[0] = FindViewById<TextView>(Resource.Id.textView2);
            textViews[1] = FindViewById<TextView>(Resource.Id.textView3);
            textViews[2] = FindViewById<TextView>(Resource.Id.textView4);
            textViews[3] = FindViewById<TextView>(Resource.Id.textView5);
            textViews[4] = FindViewById<TextView>(Resource.Id.textView6);
            textViews[5] = FindViewById<TextView>(Resource.Id.textView7);
            textViews[6] = FindViewById<TextView>(Resource.Id.textView8);
            textViews[7] = FindViewById<TextView>(Resource.Id.textView9);
            #endregion

            search = FindViewById<Button>(Resource.Id.button1);
            userInput = FindViewById<AutoCompleteTextView>(Resource.Id.autoCompleteTextView1);
            ArrayAdapter adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleDropDownItem1Line);
            userInput.Adapter = adapter;
            //userInput.SetCursorVisible(false);
            search.Click += Search_Click;

            UpdateImagesAndTexts();

            //this block used for clearing data when user log out
            var intentFilter = new IntentFilter();
            intentFilter.AddAction("com.mypackagename.ActionLogOut");
            RegisterReceiver(_logOutBroadcastReceiver, intentFilter);
        }

        /// <summary>
        /// Whene user clicks search button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Search_Click(object sender, EventArgs e)
        {
            string bookName = userInput.Text;
            await GetBooksAsync(bookName);

            Intent intent = new Intent(this, typeof(BookSearchResult));
            if (books.Count == 0)
            {
                books.Add(new Book() { Title = "There is no book with that name", AuthorId = "", Id = "" });
            }

            intent.PutExtra("book", JsonConvert.SerializeObject(books));
            StartActivity(intent);
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
                    intent.PutExtra("user", JsonConvert.SerializeObject(user));
                    StartActivity(intent);
                    break;
                case 1:
                    Toast.MakeText(this, mLeftItems[e.Position], ToastLength.Long).Show();
                    break;
                case 2:
                    Intent intent2 = new Intent(this, typeof(SettingsPage));
                    StartActivity(intent2);
                    break;
                case 3:
                    UserInfo.User = null;
                    UserInfo.Token = null;
                    UserInfo.BookId = new List<string>();
                    var broadcastIntent = new Intent();
                    broadcastIntent.SetAction("com.mypackagename.ActionLogOut");
                    SendBroadcast(broadcastIntent);
                    Intent intent1 = new Intent(this, typeof(StartGeneral));
                    StartActivity(intent1);
                    break;
            }
        }

        /// <summary>
        /// Images and texts update methods
        /// </summary>
        public async void UpdateImagesAndTexts()
        {
            var service = new BookClient(new System.Net.Http.HttpClient());
            books = (await service.GetAllBooksAsync()).ToList();

            for (int i = 0; i < books.Count; ++i)
            {
                textViews[i].Text = books[i].Title;
            }
        }

        /// <summary>
        /// Method,that send request to BookService
        /// </summary>
        /// <param name="bookName"></param>
        /// <returns></returns>
        public async Task GetBooksAsync(string bookName)
        {
            var service = new BookClient(new System.Net.Http.HttpClient());
            books = (await service.SearchBookAsync(bookName)).ToList();
        }

        /// <summary>
        /// Destroys activiy
        /// </summary>
        protected override void OnDestroy()
        {
            base.OnDestroy();
            UnregisterReceiver(_logOutBroadcastReceiver);
        }
    }
}