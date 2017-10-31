using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBookingsApp.Areas.App.Models.Management
{
    public class BookingOptionsViewModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public BookingOptionsSelectedView SelectedView { get; set; }
    }

    public enum BookingOptionsSelectedView {
        ArrivalDate,
        DepartureDate,
        BookedDate
    }

}   