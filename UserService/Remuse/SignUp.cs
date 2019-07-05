using System;
using System.Collections.Generic;
using System.Drawing;
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
    [Activity(Label = "SingUp")]
    public class SignUp : Activity
    {
        Button confirm;
        EditText name, lastname, email, username, password, birthday;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.SignUp);

            confirm = FindViewById<Button>(Resource.Id.button1);
            name = FindViewById<EditText>(Resource.Id.editText1);
            lastname = FindViewById<EditText>(Resource.Id.editText2);
            email = FindViewById<EditText>(Resource.Id.editText3);
            username = FindViewById<EditText>(Resource.Id.editText4);
            password = FindViewById<EditText>(Resource.Id.editText6);
            birthday = FindViewById<EditText>(Resource.Id.editText5);

            confirm.Click += Confirm_Click;
        }

        private void Confirm_Click(object sender, EventArgs e)
        {
            var namefail = FindViewById<TextView>(Resource.Id.textView8);        
            var lastnamefail = FindViewById<TextView>(Resource.Id.textView9);
            var emailfail = FindViewById<TextView>(Resource.Id.textView10);
            var usernamefail = FindViewById<TextView>(Resource.Id.textView11);
            var passwordfail = FindViewById<TextView>(Resource.Id.textView12);

            User.FirstName = name.Text.ToString();
            User.LastName = lastname.Text.ToString();
            User.Email = email.Text.ToString();
            User.UserName = username.Text.ToString();
            User.Password = password.Text.ToString();
            //user.BirthDay = birthday.Text.ToString();

            List<EditText> inputs = new List<EditText>() { name, lastname, email, username, password};
            TextView[] outs = new TextView[] { namefail, lastnamefail, emailfail, usernamefail, passwordfail };

            for (int i = 0; i < 5; i++)
            {
                SwitchTextValid(inputs[i], ref outs[i]);
            }
            if (namefail.Text == "" && lastnamefail.Text == "" && emailfail.Text == "" && usernamefail.Text == "" && passwordfail.Text == "")
            {
                Intent intent = new Intent(this, typeof(SignIn));
                StartActivity(intent);
            }
        }
        public static void SwitchTextValid(EditText input,ref TextView output)
        {
            if(input.Text == "")
            {
                output.Text = "*";
            }
            else
            {
                output.Text = "";
            }
        }
    }
}