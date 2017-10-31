
namespace MyBookingsApp.Areas.App.Models.Management
{
    public class PropertyViewModel
    {

        public int ID { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Website { get; set; }
        public int NumberOfRooms { get; set; }
        public int TotalBookings { get; set; }
        public float MinimumPrice { get; set; }

    }
}