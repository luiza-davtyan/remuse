using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using IdentityModel.Client;
using MyNamespace;
using Newtonsoft.Json;
using ProfileNameSpaceOurClient;
using Remuse.Activities;
using Remuse.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Remuse
{
    [Activity(Label = "SignIn")]
    public class SignIn : Activity
    {
        User userFromBase = new User();
        TextView warning;
        EditText editUsername, editPassword;
        Button signin;
        List<User> users = new List<User>();
        string usernameString, passwordString;
        //List<Claim> userClaimsFromToken;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SignIn);

            //Creating email's and password's fields
            editUsername = FindViewById<EditText>(Resource.Id.editText1);
            editPassword = FindViewById<EditText>(Resource.Id.textInputEditText1);
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
            warning = FindViewById<TextView>(Resource.Id.textView8);
            warning.Text = "";

            usernameString = editUsername.Text;
            passwordString = editPassword.Text;

            
            if (editUsername.Text == "")
            {
                warning.Text = "Please,enter username";
            }
            else if (editPassword.Text == "")
            {
                warning.Text = "Please,enter password";
            }
            else
            {
                Uri authorizationServerTokenIssuerUri = new Uri(HttpUri.AuthorizeUri + "connect/token");

                string clientId = "client";
                string clientSecret = "secret";
                string scope = "UserService";
                string username = usernameString;
                string password = passwordString;

                string token = null;
                try
                {
                    //Get token for the specified clientId, clientSecret, and scope
                    token = await RequestTokenToAuthorizationServer(authorizationServerTokenIssuerUri, clientId, scope, clientSecret, username, password);
                }
                catch { }

                //Deserialize jwt token
                AuthServerResponse authServerToken = Newtonsoft.Json.JsonConvert.DeserializeObject<AuthServerResponse>(token);

                if (authServerToken.access_token == null)
                {
                    warning.Text = "Invalid username or password";
                }
                else
                {
                    var handler = new JwtSecurityTokenHandler();
                    UserInfo.Token = authServerToken;

                    userFromBase = await GetUserByUsernameAsync(username);
                    UserInfo.User = userFromBase;
                    UserInfo.Books = await GetUserBooksByUserIdAsync(UserInfo.User.Id);

                    var server = new ProfileClient(new HttpClient());
                    var profiles = await server.GetProfilesByUserId(UserInfo.User.Id);
                    foreach(var item in profiles)
                    {
                        UserInfo.profiles.Add(new Models.Profile() { ID = item.Id, UserId = item.Id, BookId = item.BookId });
                    }
                    
                    Intent intent = new Intent(this, typeof(General));
                    intent.PutExtra("user", JsonConvert.SerializeObject(userFromBase));
                    editPassword.Text = "";
                    StartActivity(intent);
                }
            }
        }

        /// <summary>
        /// To get user from base
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<User> GetUserByUsernameAsync(string username)
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

        /// <summary>
        /// This method requests Identity Server to get access token.
        /// </summary>
        /// <param name="uriAuthorizationServerUri"></param>
        /// <param name="clientId"></param>
        /// <param name="scope"></param>
        /// <param name="clientSecret"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private static async Task<string> RequestTokenToAuthorizationServer(Uri uriAuthorizationServerUri, string clientId, string scope, string clientSecret, string username, string password)
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage();

            using (HttpClient client = new HttpClient())
            {
                // returns DiscoveryResponse 
                client.BaseAddress = uriAuthorizationServerUri;
                var disco = client.GetDiscoveryDocumentAsync(uriAuthorizationServerUri.ToString()).Result;

                HttpRequestMessage tokenRequest = new HttpRequestMessage(HttpMethod.Post, disco.TokenEndpoint);
                HttpContent httpContent = new FormUrlEncodedContent(
                    new[]
                    {
                        new KeyValuePair<string, string>("grant_type", "password"),
                        new KeyValuePair<string, string>("client_id", clientId),
                        new KeyValuePair<string, string>("scope", "BookService"),
                        new KeyValuePair<string, string>("scope", "UserService"),
                        new KeyValuePair<string, string>("client_secret", clientSecret),
                        new KeyValuePair<string, string>("username", username),
                        new KeyValuePair<string, string>("password", GetHashSha256(password))
                    });

                tokenRequest.Content = httpContent;
                responseMessage = await client.SendAsync(tokenRequest);
            }
            return await responseMessage.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// All methods must be similar to this method.
        /// </summary>
        /// <param name="authorizationServerToken"></param>
        /// <returns></returns>
        private static async Task<string> SecureWebApiCall(AuthServerResponse authorizationServerToken)
        {

            string uri = HttpUri.BookUri + "api/book";

            HttpResponseMessage responseMessage;
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authorizationServerToken.access_token);
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);
                responseMessage = await httpClient.SendAsync(request);
            }
            return await responseMessage.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// SHA256 Hash of your string.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string GetHashSha256(string text)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(text);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }

        /// <summary>
        /// To get user from base
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<Book>> GetUserBooksByUserIdAsync(int id)
        {
            HttpClient client = new HttpClient();
            try
            {
                HttpResponseMessage response = await client.GetAsync(HttpUri.ProfileApiUri + "api/profile/" + id);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                List<Book> books = JsonConvert.DeserializeObject<List<Book>>(responseBody.ToString());
                return books;
            }
            catch
            {
                throw new Exception();
            }
        }
    }
}