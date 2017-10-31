using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBookingsApp.Areas.App.Models.Management
{
    public enum BookingStatus
    {
        Confirmed,
        Cancelled,
        Provisional,
        NoShow
    }
}