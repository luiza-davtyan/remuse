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

namespace Remuse.Services
{
    public interface IHttpRequestService
    {
        Book Read(Book obyect, string endpoint, string methodType);

        Book Creat(Book obyect, string endpoint, string methodType);

        Book Update(Book obyect, string endpoint, string methodType);

        Book Delete(Book obyect, string endpoint, string methodType);

        List<Book> GetBooksByName(string name, string endpoint);
    }
}