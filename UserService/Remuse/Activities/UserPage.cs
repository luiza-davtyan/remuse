using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Widget;
using Remuse.Models;

namespace Remuse.Activities
{
    [Activity(Label = "UserPage")]
    public class UserPage : Activity
    {
        ImageView userimage;
        TextView firstname, lastname, username, birthday;
        Button books;
        List<Book> usersBooks = new List<Book>();
        List<string> mLeftItems = new List<string>();

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
            birthday = FindViewById<TextView>(Resource.Id.textView4);
            books.Click += Books_Click;

            #region menu
            DrawerLayout mDrawerLayout;

            // Array Adaper  
            ArrayAdapter mLeftAdapter;

            mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawerLayout1);
            ListView mLeftDrawer = FindViewById<ListView>(Resource.Id.leftsideview);

            mLeftItems.Add("Home");
            mLeftItems.Add("Network");
            mLeftItems.Add("Settings");

            // Set ArrayAdaper with Items  
            mLeftAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, mLeftItems);
            mLeftDrawer.Adapter = mLeftAdapter;

            mLeftDrawer.ItemClick += MLeftDrawer_ItemClick;
            #endregion

            //go to UserServer to get user's date
            //then go to BookService with token and bring info about user's books
            //usersBooks = something from book's service
        }

        private void MLeftDrawer_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Type type = typeof(General);

            int position = e.Position;
            switch (position)
            {
                case 0:
                    type = typeof(General);
                    break;
                case 1:
                    break;
                case 2:
                    //...
                    break;
            }
            Intent intent = new Intent(this, type);
            StartActivity(intent);
        }

        private  void Books_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(BookPage));
            intent.PutExtra("books", new string[] { "HIn oreri tangon","Menq enq mer sarery" });
            StartActivity(intent);
        }
        public async void Client()
        {
            //using (var client = new HttpClient())
            //{
            //    var response = await client.GetAsync("http://localhost:64319/api/book");
            //}

            //WebResponse response = request.GetResponse();//Отправляем данные и получаем ответ
            //Stream dataStream = response.GetResponseStream();//Получаем поток ответа
            //StreamReader reader = new StreamReader(dataStream);//Новый объект для чтения потока
            //string responseFromServer = reader.ReadToEnd();//Считываем в строку весь ответ сервера
            //reader.Close();//закрываем "чтеца"
            //dataStream.Close();//закрываем поток ответа
            //response.Close();//И запрос.
            //servergirq.Text = responseFromServer;
        }
    }
}