using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Widget;
using MyNamespace;
using Newtonsoft.Json;
using Remuse.Models;

namespace Remuse.Activities
{
    [Activity(Label = "SettingsPage")]
    public class SettingsPage : Activity
    {
        Button confirm;
        ImageButton changeImage;
        EditText editName, editlastName, editEmail, editUsername, editPassword;
        List<string> mLeftItems = new List<string>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.settings_page);

            editName = FindViewById<EditText>(Resource.Id.textInputEditText1);
            editlastName = FindViewById<EditText>(Resource.Id.textInputEditText2);
            editEmail = FindViewById<EditText>(Resource.Id.editText2);
            editUsername = FindViewById<EditText>(Resource.Id.textInputEditText3);
            editPassword = FindViewById<EditText>(Resource.Id.editText1);
            changeImage = FindViewById<ImageButton>(Resource.Id.imageButton1);
            confirm = FindViewById<Button>(Resource.Id.button1);

            editName.Text = UserInfo.User.Name;
            editlastName.Text = UserInfo.User.Surname;
            editEmail.Text = UserInfo.User.Email;
            editUsername.Text = UserInfo.User.Username;

            #region menu
            DrawerLayout mDrawerLayout;

            // Array Adaper  
            ArrayAdapter mLeftAdapter;

            mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawerLayout1);
            ListView mLeftDrawer = FindViewById<ListView>(Resource.Id.leftsideview);
            mLeftDrawer.SetBackgroundColor(Color.White);

            mLeftItems.Add("Home");
            mLeftItems.Add("My account");
            mLeftItems.Add("Network");

            // Set ArrayAdaper with Items  
            mLeftAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, mLeftItems);
            mLeftDrawer.Adapter = mLeftAdapter;
            mLeftDrawer.ItemClick += MLeftDrawer_ItemClick;
            #endregion

            changeImage.Click += ChangeImage_Click;
            confirm.Click += Confirm_Click;
        }

        /// <summary>
        /// Event,that works when user clicks on the item of menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MLeftDrawer_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Type type = typeof(General);
            int position = e.Position;
            switch (position)
            {
                case 0:
                    type = typeof(General);
                    break;
                case 1:
                    type = typeof(UserPage);
                    break;
                case 2:
                    Toast.MakeText(this, mLeftItems[e.Position], ToastLength.Long).Show();
                    return;
            }
            Intent intent = new Intent(this, type);
            StartActivity(intent);
        }

        /// <summary>
        /// User save his changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Confirm_Click(object sender, EventArgs e)
        {
            int id = UserInfo.User.Id;
            string response;
            if (GetHashSha256(editPassword.Text) == UserInfo.User.Password)
            {
                User updateUser = new User() { Id = id, Name = editName.Text, Surname = editlastName.Text, Email = editEmail.Text, Username = editUsername.Text, Password = editPassword.Text };
                try
                {
                    var server = new UserClient(new HttpClient());
                    //var checkValidation = await server.GetUserByUsernameAsync(updateUser.Username);

                    //if (checkValidation != null)
                    //{
                    //    Toast.MakeText(this, "That username or email is already busy", ToastLength.Long).Show();
                    //    return;
                    //}

                    response = await server.UpdateUserAsync(updateUser,UserInfo.Token.access_token);

                    UserInfo.User = await server.GetUserByUsernameAsync(updateUser.Username);
                    Toast.MakeText(this, response, ToastLength.Long).Show();
                }
                catch
                {
                    Toast.MakeText(this, "Something goes wrong", ToastLength.Long).Show();
                }
            }
            else
            {
                Toast.MakeText(this, "Please ,enter correct password", ToastLength.Long).Show();
            }
        }

        /// <summary>
        /// User changes his profile picture
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeImage_Click(object sender, EventArgs e)
        {
            
        }
        /// <summary>
        /// SHA256 Hash of your string.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string GetHashSha256(string text)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(text);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }
    }
}