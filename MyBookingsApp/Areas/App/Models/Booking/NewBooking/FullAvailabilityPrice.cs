using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBookingsApp.Areas.App.Models.Interface
{
    public class FullAvailabilityPriceItem : IAvailableRateList
    {
        public decimal Price { get; set; }
        public string RateName { get; set; }
        public int RateID { get; set; }

    }
}