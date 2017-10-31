using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBookingsApp.Areas.App.Models.Management
{
    public class AvailabilityItemViewModel
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public int Availability { get; set; }
        public bool StopSell { get; set; }
        public int ClosedToAvailability { get; set; }
        
    }
}