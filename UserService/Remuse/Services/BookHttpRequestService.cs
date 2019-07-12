using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Remuse.Models;

namespace Remuse.Services
{
    public class BookHttpRequestService //: IHttpRequestService
    {
        string uri = "http://localhost/BookService/api/book/";
        List<Book> forException = new List<Book>() { new Book() { Title = "There is no book" } };

        public Book Creat(Book obyect, string endpoint, string methodType)
        {
            throw new NotImplementedException();
        }

        public Book Delete(Book obyect, string endpoint, string methodType)
        {
            throw new NotImplementedException();
        }

        public Book Read(Book obyect, string endpoint, string methodType)
        {
            throw new NotImplementedException();
        }

        public Book Update(Book obyect, string endpoint, string methodType)
        {
            throw new NotImplementedException();
        }

        //public List<Book> GetBooksByName(string name, string endpoint)
        //{
                               
        //}

        public async Task<List<Book>> GetBooksByName(string endpoint)
        {
            string url = uri + endpoint;

            using (HttpClient client = new HttpClient())
            {
                //WebRequest request = WebRequest.Create(uri);
                //request.Method = "GET";
                //string responseFromServer;
                //try
                //{
                //    WebResponse response = request.GetResponse();//Отправляем данные и получаем ответ
                //    Stream dataStream = response.GetResponseStream();//Получаем поток ответа
                //    StreamReader reader = new StreamReader(dataStream);//Новый объект для чтения потока
                //    responseFromServer = reader.//Считываем в строку весь ответ сервера
                //    reader.Close();//закрываем "чтеца"
                //    dataStream.Close();//закрываем поток ответа
                //    response.Close();//И запрос.
                //}
                //catch
                //{
                //    return forException;
                //}
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsByteArrayAsync();
                    List<Book> res = new List<Book>();
                    res = (List<Book>)ByteArrayToObject(responseBody);
                    return res;
                }
                catch
                {
                    return forException;
                }
            }
        }
        /// <summary>
        /// Convert a byte array to an Object
        /// </summary>
        /// <param name="arrBytes"></param>
        /// <returns></returns>
        private object ByteArrayToObject(byte[] arrBytes)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            object obj = (object)binForm.Deserialize(memStream);

            return obj;
        }
    }
}