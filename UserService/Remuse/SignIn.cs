using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using MyNamespace;
using Remuse.Activities;
using System.Xml.Serialization;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace Remuse
{
    [Activity(Label = "SignIn")]
    public class SignIn : Activity
    {
        User userFromBase = new User();
        TextView correction;
        EditText username, password;
        Button signin;
        List<User> users = new List<User>();
        string usernameString, passwordString;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SignIn);

            //Creating email's and password's fields
            username = FindViewById<EditText>(Resource.Id.editText1);
            password = FindViewById<EditText>(Resource.Id.textInputEditText1);

            signin = FindViewById<Button>(Resource.Id.button1);
            signin.Click += Signin_Click;
        }

        /// <summary>
        /// Event,whene user clicks on SignIn button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Signin_Click(object sender, EventArgs e)
        {
            usernameString = username.Text;
            passwordString = password.Text;

            correction = FindViewById<TextView>(Resource.Id.textView8);
            if (username.Text == "")
            {
                correction.Text = "Please,enter email";
            }
            else if (password.Text == "")
            {
                correction.Text = "Please,enter password";
            }

            else
            {
                //var service = new UserClient(new System.Net.Http.HttpClient());
                //var result = await service.GetUserByUsernameAsync(_username);

                userFromBase = await Get(usernameString);
               
                if (userFromBase.Password == passwordString)
                {
                    Intent intent = new Intent(this, typeof(General));
                    intent.PutExtra("user", JsonConvert.SerializeObject(userFromBase));
                    StartActivity(intent);
                }
                else
                {
                    correction.Text = "Invalid email or password";
                }
                
            }
        }

        public async Task<User> Get(string username)
        {
            
            HttpClient client = new HttpClient();
            try
            {
                HttpResponseMessage response = await client.GetAsync(HttpUri.UserUri + "api/user/get/" + username);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                User user = JsonConvert.DeserializeObject<User>(responseBody.ToString());
                return user;
            }
            catch (HttpRequestException e)
            {
                throw new Exception();
            }
        }
    }
}