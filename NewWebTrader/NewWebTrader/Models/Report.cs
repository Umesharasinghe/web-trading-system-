using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewWebTrader.Models
{
    public class Report
    {
        [Required]
        public string Year { get; set; }
        [Required]
        public HttpPostedFileBase File1 { get; set; }
        [Required]
        public HttpPostedFileBase File2 { get; set; }
    }
}