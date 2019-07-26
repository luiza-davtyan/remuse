using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Webkit;
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
        Book book;
        List<string> mLeftItems = new List<string>();
        LogOutBroadcastReceiver _logOutBroadcastReceiver = new LogOutBroadcastReceiver();
        WebView webView;
        string bookUri;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.bookreader);
            book = JsonConvert.DeserializeObject<Book>(Intent.GetStringExtra("book"));
            string id = book.Id;

            switch (book.Title)
            {
                case "Pride and Prejudice":
                    bookUri = "https://drive.google.com/open?id=1rjnUj3F5Wx0QZPpmNZ6rwIM1vZ29YnhJ";
                    break;
                case "Fight Club":
                    bookUri = "https://drive.google.com/open?id=1ca-NugQ3oooT4XaBsjiQDh1C7pOpnIcy";
                    break;
                case "The Great Gatsby":
                    bookUri = "https://drive.google.com/open?id=1A2VhTWMUsYOgoNliJCQYHVd8cSiY22gk";
                    break;
                case " Ten Little Niggers":
                    bookUri = "https://drive.google.com/open?id=1MnJsZxu2AITZI19XU6tuHXv1jvxDMr8F";
                    break;
            }

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

            #region WebView
            webView = FindViewById<WebView>(Resource.Id.webView);

            //SetWebViewClient with an instance of WebViewClientClass
            webView.SetWebViewClient(new WebViewClientClass());
            webView.LoadUrl(bookUri);

            //Enabled Javascript in Websettings

            WebSettings websettings = webView.Settings;
            websettings.JavaScriptEnabled = true;

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


    internal class WebViewClientClass : WebViewClient
    {
        /// <summary>
        /// Give the host application a chance to take over the control when a new url is about to be loaded in the current WebView.
        /// </summary>
        /// <param name="view"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public override bool ShouldOverrideUrlLoading(WebView view, string url)
        {
            view.LoadUrl(url);
            return true;
        }
    }
}