using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBookingsApp.Areas.App.Models.Management
{
    public class PropertyShortItemViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
    public class PropertyShortListViewModel
    {
        public List<PropertyShortItemViewModel> PropertyList { get; set; }
        public PropertyShortItemViewModel SelectedItem { get; set; }
        public int SelectedIndex { get; set; }

    }
}