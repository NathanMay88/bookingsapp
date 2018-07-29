namespace MyBookingsApp.Areas.App.Models
{
    public class BookingRateList
    {
        public int RateID { get; set; }
        public double Price { get; set; }
        public string RateName { get; set; }
        public bool Selected { get; set; }
    }
}