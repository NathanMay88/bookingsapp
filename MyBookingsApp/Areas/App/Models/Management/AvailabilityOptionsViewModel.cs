using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBookingsApp.Areas.App.Models.Management
{
    public class AvailabilityOptionsViewModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<System.Web.Mvc.SelectListItem> RoomTypeList { get; set; }
        public string SelectedRoomTypeName { get; set; }
        public int SelectedRoomTypeID { get; set; }
    }
}