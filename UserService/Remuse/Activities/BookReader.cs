using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Remuse.Activities
{
    [Activity(Label = "BookReader")]
    public class BookReader : Activity
    {
        TextView book;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.book_read);

            book = FindViewById<TextView>(Resource.Id.textView1);
            
        }
    }
}