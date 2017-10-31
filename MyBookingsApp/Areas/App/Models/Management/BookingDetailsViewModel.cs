using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBookingsApp.Areas.App.Models.Management
{
    public class BookingDetailsViewModel
    {
        public int ID { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerAddress1 { get; set; }
        public string CustomerAddress2 { get; set; }
        public string CustomerAddress3 { get; set; }
        public string CustomerAddress4 { get; set; }
        public string CustomerCountry { get; set; }
        public string CustomerPostcode { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string CustomerEmail { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float Price { get; set; }
        public float PaidAmount { get; set; }
        public float AmountOutstanding { get; set; }
        public int RoomTypeID { get; set; }
        public string RoomTypeName { get; set; }
        public int RateID { get; set; }
        public string RateName { get; set; }
        public BookingStatus Status { get; set; }
        public List<BookingDetailsViewModel> SubBookings { get; set; }
        public string Notes { get; set; }
    }


}