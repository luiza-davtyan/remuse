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
using Remuse.Activities;

namespace Remuse
{
    [Activity(Label = "SignIn")]
    public class SignIn : Activity
    {
        TextView correction;
        EditText email, password;
        Button signin;
        int lalal = 9;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SignIn);

            //Creating email's and password's fields
            email = FindViewById<EditText>(Resource.Id.editText1);
            password = FindViewById<EditText>(Resource.Id.textInputEditText1);

            signin = FindViewById<Button>(Resource.Id.button1);
            signin.Click += Signin_Click;
        }

        private void Signin_Click(object sender, EventArgs e)
        {
            correction = FindViewById<TextView>(Resource.Id.textView8);
            if (email.Text == "")
            {
                correction.Text = "Please,enter email";
            }
            else if(password.Text == "")
            {
                correction.Text = "Please,enter password";
            }
            //write the authentication logics to go to the users page
            Intent intent = new Intent(this, typeof(UserPage));
            StartActivity(intent);
        }
    }
}