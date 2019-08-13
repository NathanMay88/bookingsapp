using MyBookingsApp.Areas.App.DataAccess;
using MyBookingsApp.Areas.App.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;


namespace MyBookingsApp.Areas.App.Models
{
    public class FullAvailabilityBookingModel : IBookingModel
    {
        public List<IAvailableRoomList> RoomList { get; set; }
        public int PID { get; set; }
        private string _bookingID;
        public string BookingID { get { return _bookingID; } }
        private DateTime _bookingdate;
        public DateTime Bookingdate { get { return _bookingdate; } }
        public int ChannelID { get; }
        public string ChannelName { get; set; }
        public decimal TotalPrice { get; set; }
        public List<IAvailableRoomList> SelectedRooms { get; set; }
        public BookingOptions Options { get; set; }

        public FullAvailabilityBookingModel(int PropertyID, DateTime StartDate, DateTime EndDate, string ChannelName, int NumberofAdults, int NumberofChildren)
        {
            _bookingID = Guid.NewGuid().ToString();
            _bookingdate = DateTime.Now;
            RoomList = GetListOfAvailableProducts(PropertyID, StartDate, EndDate, NumberofAdults, NumberofChildren);
            this.ChannelName = ChannelName;
            this.PID = PropertyID;
            this.Options = new BookingOptions { StartDate = StartDate, NumberOfNights = (EndDate - StartDate).Days, NumberOfAdults = NumberofAdults, NumberOfChildren = NumberofChildren };

        }

        public FullAvailabilityBookingModel()
        { }

        public List<IAvailableRoomList> GetListOfAvailableProducts(int PropertyID, DateTime StartDate, DateTime EndDate, int NumberofAdults, int NumberofChildren)
        {
            using (MyBookingDAL _c = new MyBookingDAL())
            {
                //Get List of Rooms and Availability
                Dictionary<int, List<Availability>> AvailableRooms = _c.GetAllAvailabilitiesForPropertty(PropertyID, StartDate, EndDate, NumberofAdults, NumberofChildren);
                //Get Room Details
                List<Roomtype> RoomDetails = _c.GetListOfRoomTypesForBooking(PropertyID);

                List<IAvailableRoomList> AvailableRoomDetails = new List<IAvailableRoomList>();
                //Combine RoomDetails and 
                foreach (var item in AvailableRooms)
                {
                    Roomtype currentRoom = RoomDetails.Single(a => a.ID == item.Key);
                  
                        //Room is Available during the time period
                        FullAvailabilityRoom room = new FullAvailabilityRoom(currentRoom.ID, StartDate,EndDate) { Available = item.Value.All(a=>a.Availability1>=1) ,Description = currentRoom.Description, MaxPeople = (int)currentRoom.MaxPeople, RoomName = currentRoom.Name, RoomImg = "", RoomsSelected = 0 };
                    if (room.Available != false)
                    {
                        room.NumAvailable = new List<System.Web.Mvc.SelectListItem>();
                        for (int i = 0; i < item.Value.Min(a=>a.Availability1) + 1; i++)
                        {
                            room.NumAvailable.Add(new System.Web.Mvc.SelectListItem() { Text = i.ToString(), Value = i.ToString(), Selected = false });
                        }
                    }
                    AvailableRoomDetails.Add(room);
                }

                return AvailableRoomDetails;
            }
        }
    }
}