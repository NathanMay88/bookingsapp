using System.Collections.Generic;

namespace MyBookingsApp.Areas.App.Models.Management
{
    public class AvailabilityGroupedViewModel
    {
        public int RoomTypeID { get; set; }
        public string RoomTypeName { get; set; }
        public List<AvailabilityItemViewModel> AvailabilityList { get; set; }
    }
}