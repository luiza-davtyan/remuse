using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Remuse.Models;

namespace Remuse
{
    [Activity(Label = "SignIn")]
    public class SignIn : Activity
    {
        TextView name, lastname, email, username;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SignIn);

            name = FindViewById<TextView>(Resource.Id.textView1);
            lastname = FindViewById<TextView>(Resource.Id.textView2);
            email = FindViewById<TextView>(Resource.Id.textView3);
            username = FindViewById<TextView>(Resource.Id.textView4);

            name.Text = User.FirstName;
            lastname.Text = User.LastName;
            email.Text = User.Email;
            username.Text = User.UserName;
        }
    }
}