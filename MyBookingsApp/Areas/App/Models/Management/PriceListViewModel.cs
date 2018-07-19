using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBookingsApp.Areas.App.Models.Management
{
    public class PriceListViewModel
    {
        public List<PriceGroupedViewModel> GroupedPriceList { get; set; }
        public int SelectedRateID { get; set; }
        public PriceOptionsViewModel PriceOptions { get; set; }
        

    }
}