using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MyNamespace;

namespace Remuse
{
    [Activity(Label = "SingUp")]
    public class SignUp : Activity
    {
        Button confirm;
        EditText name, lastname, email, username, password;
        User RegUser;
        User forException;
        
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

            confirm.Click += Confirm_Click;
        }

        /// <summary>
        /// Event,whene user clicks Confirm button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Confirm_Click(object sender, EventArgs e)
        {
            var namefail = FindViewById<TextView>(Resource.Id.textView8);        
            var lastnamefail = FindViewById<TextView>(Resource.Id.textView9);
            var emailfail = FindViewById<TextView>(Resource.Id.textView10);
            var usernamefail = FindViewById<TextView>(Resource.Id.textView11);
            var passwordfail = FindViewById<TextView>(Resource.Id.textView12);
            var messageFromDB = FindViewById<TextView>(Resource.Id.textView13);
            
            RegUser = new User();

            RegUser.Name = name.Text.ToString();
            RegUser.Surname = lastname.Text.ToString();
            RegUser.Email = email.Text.ToString();
            RegUser.Username = username.Text.ToString();
            RegUser.Password = password.Text.ToString();
            //RegUser.DateOfBirth = Convert.ToDateTime(birthday.Text);

            //switch if the user exists in database ,if no do this
            var service = new UserClient(new System.Net.Http.HttpClient());
            var result = await service.AddUserAsync(RegUser);
            

            #region validation
            List<EditText> inputs = new List<EditText>() { name, lastname, email, username, password};
            TextView[] outs = new TextView[] { namefail, lastnamefail, emailfail, usernamefail, passwordfail };

            for (int i = 0; i < 5; i++)
            {
                SwitchTextValid(inputs[i], ref outs[i]);
            }
            if (namefail.Text == "" && lastnamefail.Text == "" && emailfail.Text == "" && usernamefail.Text == "" && passwordfail.Text == ""&& result.Name != "There alread exists user with that username or email")
            {
                Intent intent = new Intent(this, typeof(SignIn));
                StartActivity(intent);
            }
            else if (result.Name == "There alread exists user with that username or email")
            {
                messageFromDB.Text = result.Name;
            }
            #endregion
        }

        /// <summary>
        /// This method switchs user input validation
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
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

        public async Task Registrate(User user)
        {
            var service = new UserClient(new System.Net.Http.HttpClient());
            await service.AddUserAsync(user);
        }
    }
}