using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBookingsApp.Areas.App.Models.Management
{
    public class AvailabilityListViewModel
    {
        public AvailabilityOptionsViewModel Options { get; set; }
        public List<AvailabilityGroupedViewModel> GroupedAvailabilityList { get; set; }   
    }
}