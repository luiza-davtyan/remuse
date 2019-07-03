using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GateWay.Routes;
using System.Net;
using System.IO;
using GateWay.Models;

namespace GateWay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        public Route bookroute;

        [HttpGet]
        public string GetAllBooks()
        {
            //bookroute = new Route()
            //{
            //    EndPoint = "",
            //    _Destination = new Destination("uri", false)
            //};
            WebRequest request = WebRequest.Create("https://localhost:44355/api/values");
            request.Method = "GET";

            WebResponse response = request.GetResponse();//Отправляем данные и получаем ответ
            Stream dataStream = response.GetResponseStream();//Получаем поток ответа
            StreamReader reader = new StreamReader(dataStream);//Новый объект для чтения потока
            string responseFromServer = reader.ReadToEnd();//Считываем в строку весь ответ сервера
            reader.Close();//закрываем "чтеца"
            dataStream.Close();//закрываем поток ответа
            response.Close();//И запрос.
            return responseFromServer;//Возвращаем полученную строку.
        }
    }
}