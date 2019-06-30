using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GateWay.Routes
{
    public class Destination
    {
        public string Uri { get; set; }

        public bool RequiresAuthentication { get; set; }   //petqa ancni authentifikacia te che?

        static HttpClient client = new HttpClient();

        private Destination()
        {
            Uri = "/";
            RequiresAuthentication = false;
        }

        public Destination(string uri)
            : this(uri, false)
        { }

        public Destination(string uri, bool requiresAuthentication)
        {
            this.Uri = uri;
            this.RequiresAuthentication = requiresAuthentication;
        }

        /// <summary>
        /// Combines the microservice’s base URI with the endpoint and query string coming from the client
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private string CreateDestinationUri(HttpRequest request)
        {
            string requestPath = request.Path.ToString();
            string queryString = request.QueryString.ToString();

            string endpoint = "";
            string[] endpointSplit = requestPath.Substring(1).Split('/');

            if (endpointSplit.Length > 1)
            {
                endpoint = endpointSplit[1];
            }

            return Uri + endpoint + queryString;
        }

        /// <summary>
        /// Sends the request to the correct URI and get the response back
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> SendRequest(HttpRequest request)
        {
            string requestContent;

            using (Stream receiveStream = request.Body)
            {
                using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                {
                    requestContent = readStream.ReadToEnd();
                }
            }

            HttpClient client = new HttpClient();
            HttpRequestMessage newRequest = new HttpRequestMessage(new HttpMethod(request.Method), CreateDestinationUri(request));
            HttpResponseMessage response = await client.SendAsync(newRequest);

            return response;
        }
    }
}