using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBookingsApp.Areas.App.Models
{
    public class BookingOptions
    {
        public DateTime StartDate { get; set; }
        public int NumberOfNights { get; set; }
        public int NumberOfAdults { get; set; }
        public int NumberOfChildren { get; set; }
        
    }
}