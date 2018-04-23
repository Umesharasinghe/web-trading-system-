using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewWebTrader.Models
{
    public class EnterDataROC
    {
        [Required]
        public string Market { get; set; }
        [Required]
        public DateTime SelectDate { get; set; }
        [Required]
        public int period { get; set; }
    }
}