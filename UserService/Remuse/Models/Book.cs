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
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Remuse.Models
{
    public class Book
    {
       // [BsonId]
       // [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        //[JsonProperty("Title")]
        //[BsonRequired]
        public string Title { get; set; }

        public string Content { get; set; }

        public string Description { get; set; }

        public int AuthorId { get; set; }

        public int Year { get; set; }

        public string Path { get; set; }

        //public virtual List<ObjectId> GenreIds { get; set; }
    }
}