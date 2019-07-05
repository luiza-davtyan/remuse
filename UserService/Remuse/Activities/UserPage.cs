using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Widget;

namespace Remuse.Activities
{
    [Activity(Label = "UserPage")]
    public class UserPage : Activity
    {
        ImageView userimage;
        TextView firstname, lastname, username;
        Button books;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Userpage);

            userimage = FindViewById<ImageView>(Resource.Id.imageView1);
            userimage.SetImageResource(Resource.Drawable.icon);

            firstname = FindViewById<TextView>(Resource.Id.textView1);
            lastname = FindViewById<TextView>(Resource.Id.textView2);
            username = FindViewById<TextView>(Resource.Id.textView3);
            books = FindViewById<Button>(Resource.Id.button1);
            books.Click += Books_Click;

            
            //go to UserServer to get user's date
        }

        private void Books_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}