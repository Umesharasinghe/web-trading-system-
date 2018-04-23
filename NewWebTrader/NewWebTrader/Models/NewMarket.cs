using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewWebTrader.Models
{
    public class NewMarket
    {
        [Key]
        public int Id { get; set; }
        public string type { get; set; }
        public DateTime date { get; set; }
        public DateTime time { get; set; }
        public float openingPrice { get; set; }
        public float highestPrice { get; set; }
        public float closingPrice { get; set; }
        public float lowestPrice { get; set; }
        public float temp { get; set; }
    }
}