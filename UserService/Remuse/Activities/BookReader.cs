﻿using System;
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

            #region menu
            DrawerLayout mDrawerLayout;

            ArrayAdapter mLeftAdapter;

            mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.mydrawer);
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
                    type = typeof(General);
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
        /// Destroys activiy
        /// </summary>
        protected override void OnDestroy()
        {
            base.OnDestroy();
            UnregisterReceiver(_logOutBroadcastReceiver);
        }
    }
}