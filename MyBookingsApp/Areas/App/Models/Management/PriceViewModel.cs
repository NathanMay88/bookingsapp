using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBookingsApp.Areas.App.Models.Management
{
    public class PriceViewModel
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public float Price { get; set; }
        public bool StopSell { get; set; }
        public int MinimumStay { get; set; }

    }
}