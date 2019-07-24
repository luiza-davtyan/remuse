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
using System.IO;

namespace Remuse.Activities
{
    [Activity(Label = "BookReader")]
    public class BookReader : Activity
    {
        TextView reading;
        ScrollView scrollView;
        Book book;
        List<string> mLeftItems = new List<string>();
        LogOutBroadcastReceiver _logOutBroadcastReceiver = new LogOutBroadcastReceiver();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.book_read);
            book = JsonConvert.DeserializeObject<Book>(Intent.GetStringExtra("book"));
            reading = FindViewById<TextView>(Resource.Id.textView1);
            scrollView = FindViewById<ScrollView>(Resource.Id.scrollView1);

            reading.Text = book.Content;
            string filePath = @"C:\Users\Serob\Desktop\Front3\UserService\BookService\Pdf\fight_club.pdf";
            string fileName = "fight_club.pdf";

            var bytes = File.ReadAllBytes(filePath);
            string externalStorageState = global::Android.OS.Environment.ExternalStorageState;
            var externalPath = global::Android.OS.Environment.ExternalStorageDirectory.Path + "/" + global::Android.OS.Environment.DirectoryDownloads + "/" + fileName;
            File.WriteAllBytes(externalPath, bytes);

            var bookreader = File.CreateText(externalPath);
            Stream stream = bookreader.BaseStream;
            StreamReader reader = new StreamReader(stream);
            string text = reader.ReadToEnd();

            #region menu
            DrawerLayout mDrawerLayout;

            ArrayAdapter mLeftAdapter;

            mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.mydrawer);
            ListView mLeftDrawer = FindViewById<ListView>(Resource.Id.leftsideview);
            mLeftDrawer.SetBackgroundColor(Color.White);

            mLeftItems.Add("Home");
            mLeftItems.Add("My account");
            mLeftItems.Add("Network");
            mLeftItems.Add("Settings");
            mLeftItems.Add("Log out");

            // Set ArrayAdaper with Items  
            mLeftAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, mLeftItems);
            mLeftDrawer.Adapter = mLeftAdapter;

            mLeftDrawer.ItemClick += MLeftDrawer_ItemClick;
            #endregion

            #region Music
            //var play = FindViewById<Button>(Resource.Id.button1);
            //var pause = FindViewById<Button>(Resource.Id.button2);
            //var stop = FindViewById<Button>(Resource.Id.button3);

            //play.Click += (sender, args) => SendAudioCommand(StreamingBackgroundService.ActionPlay);
            //pause.Click += (sender, args) => SendAudioCommand(StreamingBackgroundService.ActionPause);
            //stop.Click += (sender, args) => SendAudioCommand(StreamingBackgroundService.ActionStop);

            #endregion

            var intentFilter = new IntentFilter();
            intentFilter.AddAction("com.mypackagename.ActionLogOut");
            RegisterReceiver(_logOutBroadcastReceiver, intentFilter);
        }

        private void SendAudioCommand(string action)
        {
            var intent = new Intent(action);
            StartService(intent);
        }
        /// <summary>
        ///  Event,that works when user clicks on the item of menu
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
                    break;
                case 1:
                    type = typeof(UserPage);
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
                    UserInfo.BookId = new List<string>();
                    var broadcastIntent = new Intent();
                    broadcastIntent.SetAction("com.mypackagename.ActionLogOut");
                    SendBroadcast(broadcastIntent);
                    type = typeof(StartGeneral);
                    break;
            }
            Intent intent = new Intent(this, type);
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
    }
}