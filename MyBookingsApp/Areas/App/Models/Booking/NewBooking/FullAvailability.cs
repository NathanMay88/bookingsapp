using MyBookingsApp.Areas.App.DataAccess;
using MyBookingsApp.Areas.App.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBookingsApp.Areas.App.Models
{
    public class FullAvailabilityRoom : IAvailableRoomList
    {
        List<IAvailableRateList> _availableRates;
        public List<IAvailableRateList> AvailableRates { get { return _availableRates; } }
        int _roomID;
        public int RoomID { get { return _roomID; } }
        public string RoomName { get; set; }
        public string Description { get; set; }
        public int MaxPeople { get; set; }
        public string RoomImg { get; set; }
        public bool Available { get; set; }
        public List<SelectListItem> NumAvailable { get; set; }
        public int RoomsSelected { get; set; }

        public FullAvailabilityRoom(int RoomID, DateTime StartDate, DateTime EndDate)
        {
            _availableRates = new List<IAvailableRateList>();
            _roomID = RoomID;
            GetPrices(RoomID, StartDate, EndDate);
        }

        public void GetPrices(int RoomID, DateTime StartDate, DateTime EndDate)
        {
            using (MyBookingDAL _c = new MyBookingDAL())
            {
                Dictionary<Rate, List<Price>> currentRates = _c.GetPricesForDates(RoomID, StartDate, EndDate);
                if (currentRates != null)
                {
                    foreach (var item in currentRates)
                    {
                        _availableRates.Add(new FullAvailabilityPriceItem() { Price = item.Value.Sum(a => a.Price1), RateID = item.Key.ID, RateName = item.Key.Name });
                    }
                }
                
            }

        }
    }
}
