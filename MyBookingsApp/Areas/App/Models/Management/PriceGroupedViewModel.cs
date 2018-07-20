using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBookingsApp.Areas.App.Models.Management
{
    public class PriceGroupedViewModel
    {
        public List<PriceViewModel> PriceList { get; set; }
        public int RoomTypeID { get; set; }
        public string RoomTypeName { get; set; }
        public int RateID { get; set; }
        public string RateName { get; set; }

    }
}