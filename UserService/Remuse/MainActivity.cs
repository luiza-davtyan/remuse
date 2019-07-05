﻿using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;

namespace Remuse
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        Button signin;
        Button signup;

        ImageView underimage;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            underimage = FindViewById<ImageView>(Resource.Id.imageView1);
            underimage.SetImageResource(Resource.Drawable.icon);

            signin = FindViewById<Button>(Resource.Id.button1);
            signup = FindViewById<Button>(Resource.Id.button2);

            signin.Click += Signin_Click;
            signup.Click += Signup_Click;

            
        }

        private void Signup_Click(object sender, System.EventArgs e)
        {
            //SetContentView(Resource.Layout.SignUp);
            Intent intent = new Intent(this, typeof(SignUp));
            StartActivity(intent);
        }

        private void Signin_Click(object sender, System.EventArgs e)
        {
            //SetContentView(Resource.Layout.SignIn);
            Intent intent = new Intent(this, typeof(SignIn));
            StartActivity(intent);
        }
    }
}