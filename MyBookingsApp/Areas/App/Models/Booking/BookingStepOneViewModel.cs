using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBookingsApp.Areas.App.Models.BookingModule
{
    public class BookingStepOneViewModel
    {
        //Make Decision on Roomtype and Rate
        //List of Available Rooms, and Associated Rates
        public List<BookingRoomList> RoomList { get; set; }
        public BookingOptions Options { get; set; }
        public int PID { get; set; }
        private string _bookingID;
        public string BookingID { get { return _bookingID; } }
        

        public BookingStepOneViewModel() {
            _bookingID = Guid.NewGuid().ToString();
        }

        public void SetBookingID(string bookid)
        {
            _bookingID = bookid;
        }
        
}
}