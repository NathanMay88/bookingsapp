using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBookingsApp.Areas.App.Models.Management
{
    public class PriceOptionsViewModel
    {
        public List<System.Web.Mvc.SelectListItem> RateList { get; set; }
        public List<System.Web.Mvc.SelectListItem> RoomTypeList { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}