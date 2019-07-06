using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Remuse.Models;

namespace Remuse.Activities
{
    [Activity(Label = "UserPage")]
    public class UserPage : Activity
    {
        ImageView userimage;
        TextView firstname, lastname, username, servergirq;
        Button books;
        List<Book> usersBooks = new List<Book>();
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
            servergirq = FindViewById<TextView>(Resource.Id.textView4);
            books.Click += Books_Click;


            //go to UserServer to get user's date
            //then go to BookService with token and bring info about user's books
            //usersBooks = something from book's service
        }

        private  void Books_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(BookPage));
            //intent.PutExtra();
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