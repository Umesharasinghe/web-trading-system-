using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewWebTrader.Models
{
    public class Article
    {
        public string author { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public string urlToImage { get; set; }
        public DateTime publishedAt { get; set; }
    }
}