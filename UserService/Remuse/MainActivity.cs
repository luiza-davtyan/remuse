using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Android.Content;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Support.V4.Widget;
using System;
using Remuse.Activities;
using System.Threading;
using Newtonsoft.Json;
using Android.Graphics;
using System.Linq;
using System.Net.Http;

namespace Remuse
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        Button search;
        ImageView[] bookImages = new ImageView[8];
        TextView[] textViews = new TextView[8];
        List<string> mLeftItems = new List<string>();
        AutoCompleteTextView userInput;
        List<Book> books = new List<Book>();

        string[] complete = new string[] { "barev", "aca", "this", "Armenia", "Destination" };

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.startgeneral);

            #region menu
            DrawerLayout mDrawerLayout;

            // Array Adaper  
            ArrayAdapter mLeftAdapter;

            mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawerLayout1);
            ListView mLeftDrawer = FindViewById<ListView>(Resource.Id.leftsideview);
            mLeftDrawer.SetBackgroundColor(Color.White);


            mLeftItems.Add("Log In");
            mLeftItems.Add("Network");
            mLeftItems.Add("Settings");

            // Set ArrayAdaper with Items  
            mLeftAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, mLeftItems);
            mLeftDrawer.Adapter = mLeftAdapter;
            mLeftDrawer.ItemClick += MLeftDrawer_ItemClick;

            #endregion

            #region Page's images...
            bookImages[0] = FindViewById<ImageView>(Resource.Id.imageView1);
            bookImages[1] = FindViewById<ImageView>(Resource.Id.imageView2);
            bookImages[2] = FindViewById<ImageView>(Resource.Id.imageView3);
            bookImages[3] = FindViewById<ImageView>(Resource.Id.imageView4);
            bookImages[4] = FindViewById<ImageView>(Resource.Id.imageView5);
            bookImages[5] = FindViewById<ImageView>(Resource.Id.imageView6);
            bookImages[6] = FindViewById<ImageView>(Resource.Id.imageView7);
            bookImages[7] = FindViewById<ImageView>(Resource.Id.imageView8);

            textViews[0] = FindViewById<TextView>(Resource.Id.textView2);
            textViews[1] = FindViewById<TextView>(Resource.Id.textView3);
            textViews[2] = FindViewById<TextView>(Resource.Id.textView4);
            textViews[3] = FindViewById<TextView>(Resource.Id.textView5);
            textViews[4] = FindViewById<TextView>(Resource.Id.textView6);
            textViews[5] = FindViewById<TextView>(Resource.Id.textView7);
            textViews[6] = FindViewById<TextView>(Resource.Id.textView8);
            textViews[7] = FindViewById<TextView>(Resource.Id.textView9);
            #endregion

            search = FindViewById<Button>(Resource.Id.button1);
            userInput = FindViewById<AutoCompleteTextView>(Resource.Id.autoCompleteTextView1);

            UpdateImagesAndTexts();

            ArrayAdapter adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleDropDownItem1Line, complete);
            userInput.Adapter = adapter;
            search.Click += Search_Click;
        }

        /// <summary>
        /// Event,whene user clicks search button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Search_Click(object sender, EventArgs e)
        {
            string bookName = userInput.Text;
            await GetBookAsync(bookName);

            Intent intent = new Intent(this, typeof(BookSearchResult));
            intent.PutExtra("book", JsonConvert.SerializeObject(books));
            StartActivity(intent);
        }

        /// <summary>
        /// Event,that works when user clicks on the item of menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MLeftDrawer_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Type type = typeof(SignIn);

            int position = e.Position;
            switch (position)
            {
                case 0:
                    type = typeof(StartGeneral);
                    Intent intent = new Intent(this, type);
                    StartActivity(intent);
                    break;
                case 1:
                    Toast.MakeText(this, mLeftItems[e.Position], ToastLength.Long).Show();
                    break;
                default:
                    Toast.MakeText(this, mLeftItems[e.Position], ToastLength.Long).Show();
                    break;
            }
        }

        /// <summary>
        /// Images and texts update methods
        /// </summary>
        public async void UpdateImagesAndTexts()
        {
            var service = new BookClient(new System.Net.Http.HttpClient());
            books = (await service.GetAllBooksAsync()).ToList();

            for(int i = 0;i < books.Count; ++i)
            {
                textViews[i].Text = books[i].Title;
            }
        }

        /// <summary>
        /// Method,that send request to BookService
        /// </summary>
        /// <param name="bookName"></param>
        /// <returns></returns>
        public async Task GetBookAsync(string bookName)
        {
            var service = new BookClient(new System.Net.Http.HttpClient());
            books = (await service.SearchBookAsync(bookName)).ToList();
        }
    }
}