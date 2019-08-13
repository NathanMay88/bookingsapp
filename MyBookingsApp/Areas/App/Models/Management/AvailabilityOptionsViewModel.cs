using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBookingsApp.Areas.App.Models.Management
{
    public class AvailabilityOptionsViewModel
    {
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<System.Web.Mvc.SelectListItem> RoomTypeList { get; set; }
        public string SelectedRoomTypeName { get; set; }
        public int SelectedRoomTypeID { get; set; }
    }
}