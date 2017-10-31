using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBookingsApp.Areas.App.Models.Management
{
    public class RatePlanViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ParentRateID { get; set; }
        public List<SelectListItem> ParentRateList { get; set; }
        public FormulaViewModel Formula { get; set; }
        private List<SelectListItem> _selectedDays;
        public List<SelectListItem> AvailableDays
        {
            get
            {
                if (_selectedDays == null)
                {
                    _selectedDays = new List<SelectListItem>();
                    for (int i = 0; i <= 6; i++)
                    {
                        _selectedDays.Add(new SelectListItem() { Selected = false, Text = ((DayOfWeek)(i == 6 ? 0 : i + 1)).ToString(), Value = (i == 6 ? 0 : i + 1).ToString() });
                    }
                }
                return _selectedDays;
            }
            set
            {
                _selectedDays = value;
            }
        }
        public int ClosedToArrival { get; set; }

    }

    public class FormulaViewModel
    {
        public FormulaValueSign Sign { get; set; }
        public FormulaMultiplyer Percentage { get; set; }
        public int Value { get; set; }
    }

    public enum FormulaValueSign
    {
        Plus,
        Minus
    }

    public enum FormulaMultiplyer {
        Amount,
        Percentage
    }
}