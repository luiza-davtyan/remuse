using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Widget;
using MyNamespace;
using Newtonsoft.Json;
using Remuse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Remuse.Activities
{
    [Activity(Label = "UserPage")]
    public class UserPage : Activity
    {
        ImageView userimage;
        TextView firstname, lastname, username,email;
        Button books,editProfile,editBookList;
        List<Book> usersBooks = new List<Book>();
        List<string> mLeftItems = new List<string>();
        User user;
        LogOutBroadcastReceiver _logOutBroadcastReceiver = new LogOutBroadcastReceiver();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Userpage);
            //user = JsonConvert.DeserializeObject<User>(Intent.GetStringExtra("user"));
            user = UserInfo.User;

            userimage = FindViewById<ImageView>(Resource.Id.imageView1);
            userimage.SetImageResource(Resource.Drawable.icon);

            firstname = FindViewById<TextView>(Resource.Id.textView1);
            lastname = FindViewById<TextView>(Resource.Id.textView2);
            username = FindViewById<TextView>(Resource.Id.textView3);
            books = FindViewById<Button>(Resource.Id.button1);
            editProfile = FindViewById<Button>(Resource.Id.button2);
            editBookList = FindViewById<Button>(Resource.Id.button3);

            books.Click += Books_Click;
            editProfile.Click += EditProfile_Click;
            editBookList.Click += EditBookList_Click;

            firstname.Text = user.Name;
            lastname.Text = user.Surname;
            username.Text = user.Username;

            #region menu
            DrawerLayout mDrawerLayout;

            // Array Adaper  
            ArrayAdapter mLeftAdapter;

            mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawerLayout1);
            ListView mLeftDrawer = FindViewById<ListView>(Resource.Id.leftsideview);
            mLeftDrawer.SetBackgroundColor(Color.White);

            mLeftItems.Add("Home");
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
        /// Event for edit book click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void EditBookList_Click(object sender, EventArgs e)
        {
            UserInfo.Books = await GetUserBooksByUserIdAsync(UserInfo.User.Id);
            Intent intent = new Intent(this, typeof(BookListEditDelete));
            StartActivity(intent);
        }

        /// <summary>
        /// Event for edit profile click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditProfile_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(SettingsPage));
            StartActivity(intent);
        }

        /// <summary>
        /// Event,that works when user clicks on the item of menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MLeftDrawer_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            int position = e.Position;
            switch (position)
            {
                case 0:
                    Intent intent = new Intent(this, typeof(General));
                    StartActivity(intent);
                    break;
                case 1:
                    Toast.MakeText(this, mLeftItems[e.Position], ToastLength.Long).Show();
                    break;
                case 2:
                    Intent intent1 = new Intent(this, typeof(SettingsPage));
                    StartActivity(intent1);
                    break;
                case 3:
                    UserInfo.User = null;
                    UserInfo.Token = null;
                    UserInfo.BookId = new List<string>();
                    var broadcastIntent = new Intent();
                    broadcastIntent.SetAction("com.mypackagename.ActionLogOut");
                    SendBroadcast(broadcastIntent);
                    Intent intent3 = new Intent(this, typeof(StartGeneral));
                    StartActivity(intent3);
                    break;
            }
        }

        /// <summary>
        /// Ebent,when user clicks Books button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private  void Books_Click(object sender, EventArgs e)
        {          
            if (UserInfo.Books.Count == 0)
            {
                Toast.MakeText(this, "Your book's list is empty", ToastLength.Long).Show();
            }
            else
            {
                Intent intent = new Intent(this, typeof(UserBooks));
                StartActivity(intent);
            }
        }

        /// <summary>
        /// To get user from base
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<Book>> GetUserBooksByUserIdAsync(int id)
        {
            HttpClient client = new HttpClient();
            try
            {
                HttpResponseMessage response = await client.GetAsync(HttpUri.ProfileApiUri + "api/profile/" + id);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                List<Book> books = JsonConvert.DeserializeObject<List<Book>>(responseBody.ToString());
                return books;
            }
            catch
            {
                throw new Exception();
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
        /// Gets the books
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<List<Book>> Get(string booktitle)
        {
            HttpClient client = new HttpClient();
            try
            {
                HttpResponseMessage response = await client.GetAsync(HttpUri.BookUri + "api/book/search/" + booktitle);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                List<Book> book = JsonConvert.DeserializeObject<List<Book>>(responseBody.ToString());
                return book;
            }
            catch
            {
                throw new Exception();
            }
        }
    }
}