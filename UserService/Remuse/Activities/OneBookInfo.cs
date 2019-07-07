using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using Remuse.Models;
using Xamarin.Android;
namespace Remuse.Activities
{
    [Activity(Label = "BookNumberN")]
    public class OneBookInfo : Activity
    {
        Book selectedBook;
        TextView enteredbook,author,genre,year,description;
        ScrollView scroll;
        Button read;
        List<string> mLeftItems = new List<string>();

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
            enteredbook.Text = selectedBook.Title + (string)selectedBook.Id;
            author.Text = author.Text + selectedBook.Author;
            genre.Text = genre.Text + selectedBook.Genre;
            year.Text = year.Text + selectedBook.Year;
            description.Text = selectedBook.Description;

            read.Click += Read_Click;

            #region menu
            DrawerLayout mDrawerLayout;

            // Array Adaper  
            ArrayAdapter mLeftAdapter;

            mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawerLayout1);
            ListView mLeftDrawer = FindViewById<ListView>(Resource.Id.leftsideview);

            mLeftItems.Add("My account");
            mLeftItems.Add("Network");
            mLeftItems.Add("Settings");

            // Set ArrayAdaper with Items  
            mLeftAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, mLeftItems);
            mLeftDrawer.Adapter = mLeftAdapter;
            mLeftDrawer.ItemClick += MLeftDrawer_ItemClick;
            #endregion
        }

        private void MLeftDrawer_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Type type = typeof(UserPage);

            int position = e.Position;
            switch (position)
            {
                case 0:
                    break;
                case 1:
                    //Network
                    break;
                case 2:
                    //Settings
                    break;
            }
            Intent intent = new Intent(this, type);
            StartActivity(intent);
        }

        private void Read_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this,typeof(BookReader));
            StartActivity(intent);
        }
    }
}