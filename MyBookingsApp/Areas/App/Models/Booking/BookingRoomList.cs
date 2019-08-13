using MyBookingsApp.Areas.App.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBookingsApp.Areas.App.Models
{
    public class BookingRoomList
    {
        public List<BookingRateList> AvailableRates { get; set; }
        public string RoomName { get; set; }
        public int RoomID { get; set; }
        public string RoomImg { get; set; }
        public string Description { get; set; }
        public int MaxOccupancy { get; set; }
        public bool Selected { get; set; }

     
    }
}