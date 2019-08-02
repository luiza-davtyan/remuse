using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Newtonsoft.Json;
using ProfileNameSpaceOurClient;
using Remuse.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Remuse.Activities
{
    [Activity(Label = "BookLisEditDelete")]
    public class BookListEditDelete : ListActivity
    {
        List<Book> usersbooks; //get this from BooksService or activity...
        List<string> mLeftItems = new List<string>();
        PopupMenu menu;
        List<string> titles = new List<string>();
        int profileId,profilePosition;
        ProfileClient server = new ProfileClient(new HttpClient());

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            UserInfo.Books = await GetUserBooksByUserIdAsync(UserInfo.User.Id);
            usersbooks = UserInfo.Books;
           
            foreach (Book book in usersbooks)
            {
                titles.Add(book.Title);
            }

            ListAdapter = new ArrayAdapter<string>(this, Resource.Layout.list_item, titles);
            ListView.TextFilterEnabled = true;
            ListView.ItemClick += ListView_ItemClick;
        }

        private  void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs args)
        {
            profileId = UserInfo.profiles[args.Position].ID;
            profilePosition = args.Position;

            menu = new PopupMenu(this,ListView.GetChildAt(args.Position));
            menu.Inflate(Resource.Menu.popup_menu);

            menu.MenuItemClick += Menu_MenuItemClick;
            menu.Show();
        }

        private void Menu_MenuItemClick(object sender, PopupMenu.MenuItemClickEventArgs e)
        {
            if (e.Item.TitleFormatted.ToString() == "Confirm")
            {
                try
                {
                    ConvertProfile();
                    Thread.Sleep(1000);
                    foreach(var item in UserInfo.profiles)
                    {
                        if(profileId == item.ID)
                        {
                            DeleteProfile(profileId);
                            UserInfo.profiles = new List<Models.Profile>();
                            Toast.MakeText(this, "Book has been removed from your list", ToastLength.Long).Show();
                            return;
                        }
                        else
                        {
                            Toast.MakeText(this, "That book has already removed from your list", ToastLength.Long).Show();
                            return;
                        }
                    }
                }
                catch
                {
                    Toast.MakeText(this, "Something goes wrong", ToastLength.Long).Show();
                }
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// Delete profile from user's profile
        /// </summary>
        /// <param name="id"></param>
        private void DeleteProfile(int id)
        {
            var server = new ProfileClient(new System.Net.Http.HttpClient());
            server.DeleteProfileASync(id);
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

        public async void ConvertProfile()
        {
            var profiles = await server.GetProfilesByUserId(UserInfo.User.Id);
            UserInfo.profiles = new List<Models.Profile>();
            foreach (var item in profiles)
            {
                UserInfo.profiles.Add(new Models.Profile() { ID = item.Id, UserId = item.Id, BookId = item.BookId });
            }
        }
    }
}