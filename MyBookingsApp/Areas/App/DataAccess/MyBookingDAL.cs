using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyBookingsApp.Areas.App.Models;
using Microsoft.AspNet.Identity;

namespace MyBookingsApp.Areas.App.DataAccess
{
    public class MyBookingDAL : IDisposable
    {
        private MyBookingAppDBDataContext _c;
        private string _userID;
        public Exception Error;

        private int _propertyID;
        public int PID
        {
            get { return _propertyID; }

        }

        private int _currentProperty;
        public int CurrentProperty
        {
            get
            {
                RestoreSession();
                return _currentProperty;
            }
            set
            {
                _currentProperty = value;
                SaveCurrentPID(value);
            }
        }

        public MyBookingDAL()
        {
            _c = new MyBookingAppDBDataContext();
            _userID = HttpContext.Current.User.Identity.GetUserId();
        }

        #region Company

        public int AddCompany(Company NewCompany)
        {
            try
            {
                _c.Companies.InsertOnSubmit(NewCompany);
                _c.SubmitChanges();
                return NewCompany.ID;
            }
            catch (Exception ex)
            {
                Error = ex;
                return -1;
            }
        }

        public Company GetCompanyForPropertyID(int PropertyID)
        {
            try
            {
                return _c.Properties.Single(a => a.ID == PropertyID).Company;
            }
            catch (Exception ex)
            {
                Error = ex;
                return null;
            }
        }

        #endregion

        #region Property

        public Property GetSinglePropertyByID(int ID)
        {
            try
            {
                return _c.User_Properties.SingleOrDefault(b => b.PropertyID == ID && b.User_ID == _userID).Property;
            }
            catch (Exception ex)
            {
                Error = ex;
                return null;
            }

        }

        public List<Property> GetListOfProperties()
        {
            try
            {
                return _c.User_Properties.Where(a => a.User_ID == _userID).Select(b => b.Property).ToList();
            }
            catch (Exception ex)
            {
                Error = ex;
                return null;
            }
        }

        public bool AddProperty(Property NewProperty)
        {
            try
            {
                _c.Properties.InsertOnSubmit(NewProperty);
                _c.User_Properties.InsertOnSubmit(new User_Property() { Property = NewProperty, User_ID = _userID });
                _c.SubmitChanges();
                _propertyID = NewProperty.ID;
                return true;
            }
            catch (Exception ex)
            {
                Error = ex;
                return false;
            }
        }

        /// <summary>
        /// Returns the first PropertyID for the given userID, This routine ignores the _userID held within the instance.
        /// </summary>
        /// <param name="curUserID">UserID to test</param>
        /// <returns>-1 if no Property, PropertyID of the first Property</returns>
        public int CheckForProperty(string curUserID)
        {
            try
            {
                return _c.User_Properties.FirstOrDefault(a => a.User_ID == curUserID).PropertyID;
            }
            catch (Exception ex)
            {

                Error = ex;
                return -1;
            }
        }


        #endregion

        #region Room Types

        public Roomtype GetSingleRoomTypeByID(int ID)
        {
            try
            {
                return _c.Roomtypes.Single(a => a.ID == ID && a.Property.User_Properties.Single(b => b.PropertyID == a.PropertyID).User_ID == _userID);
            }
            catch (Exception ex)
            {
                Error = ex;
                return null;
            }

        }

        public List<Roomtype> GetListOfRoomTypes(int PropertyID)
        {
            try
            {
                if (_c.User_Properties.Where(a => a.User_ID == _userID && a.PropertyID == PropertyID).Count() == 1)
                {
                    return _c.Roomtypes.Where(a => a.PropertyID == PropertyID).ToList();
                }
                Error = new Exception("There are no Room Types associated with your logon");
                return null;
            }
            catch (Exception ex)
            {
                Error = ex;
                return null;
            }
        }

        public bool AddRoomType(Roomtype NewRoomType)
        {
            try
            {
                _c.Roomtypes.InsertOnSubmit(NewRoomType);
                _c.SubmitChanges();
                CreateDefaultAvailability(NewRoomType);
                CreateDefaultPricesOnRoom(NewRoomType.ID);
                return true;
            }
            catch (Exception ex)
            {
                Error = ex;
                return false;
            }
        }

        public bool RemoveRoomType(Roomtype _roomType)
        {
            try
            {
                _c.Roomtypes.DeleteOnSubmit(_roomType);
                _c.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Error = ex;
                return false;
            }
        }

        public bool RemoveRoomTypeByID(int ID)
        {
            try
            {
                if (_c.Roomtypes.Any(a => a.ID == ID && a.Property.User_Properties.Any(b => b.User_ID == _userID && b.PropertyID == a.PropertyID)))
                {
                    _c.Roomtypes.DeleteOnSubmit(_c.Roomtypes.Single(a => a.ID == ID));
                    _c.SubmitChanges();
                    return true;
                }
                Error = new Exception("Room Type is not part of Users Logon");
                return false;
            }
            catch (Exception ex)
            {
                Error = ex;
                return false;
            }
        }

        public bool EditRoomType(Roomtype EdittedRoomType, int ID)
        {
            try
            {
                Roomtype originalRoomType = _c.Roomtypes.First(a => a.ID == ID);
                originalRoomType.DefaultAllotment = EdittedRoomType.DefaultAllotment;
                originalRoomType.Formula = EdittedRoomType.Formula;
                originalRoomType.MaxPeople = EdittedRoomType.MaxPeople;
                originalRoomType.MinPeople = EdittedRoomType.MinPeople;
                originalRoomType.MinPrice = EdittedRoomType.MinPrice;
                originalRoomType.Name = EdittedRoomType.Name;
                originalRoomType.ParentRoomID = EdittedRoomType.ParentRoomID;
                originalRoomType.Description = EdittedRoomType.Description;
                _c.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Error = ex;
                return false;
            }
        }
        #endregion

        #region Rates

        public Rate GetSingleRateByID(int ID)
        {
            try
            {
                if (_c.User_Properties.SingleOrDefault(a => a.User_ID == _userID && _c.Rates.Single(b => b.ID == ID).PropertyID == a.PropertyID) != null)
                {
                    //Currently Logged in User has access to Rate
                    return _c.Rates.Single(a => a.ID == ID);
                }
                //Log Unauthorised attempt here.
                return null;
            }
            catch (Exception ex)
            {
                Error = ex;
                return null;
            }
        }

        public List<Rate> GetListOfRates(int PropertyID)
        {
            try
            {
                if (_c.User_Properties.Any(a => a.PropertyID == PropertyID && a.User_ID == _userID))
                {
                    return _c.Rates.Where(a => a.PropertyID == PropertyID).ToList();
                }
                return null;
            }
            catch (Exception ex)
            {
                Error = ex;
                throw;
            }
        }

        public bool AddRate(Rate NewRate)
        {
            try
            {
                _c.Rates.InsertOnSubmit(NewRate);
                _c.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Error = ex;
                return false;
            }
        }

        public bool DeleteRate(Rate OldRate)
        {
            try
            {
                if (_c.User_Properties.SingleOrDefault(a => a.User_ID == _userID && _c.Rates.Single(b => b.ID == OldRate.ID).PropertyID == a.PropertyID) != null)
                {
                    _c.Rates.DeleteOnSubmit(OldRate);
                    _c.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Error = ex;
                return false;
            }
        }

        public bool DeleteRateByID(int ID)
        {
            try
            {
                if (_c.User_Properties.SingleOrDefault(a => a.User_ID == _userID && _c.Rates.Single(b => b.ID == ID).PropertyID == a.PropertyID) != null)
                {
                    _c.Rates.DeleteOnSubmit(_c.Rates.Single(a => a.ID == ID));
                    _c.SubmitChanges();
                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {
                Error = ex;
                return false;
            }
        }

        public bool EditRate(Rate EdittedRate, int ID)
        {
            try
            {
                if (_c.User_Properties.SingleOrDefault(a => a.User_ID == _userID && _c.Rates.Single(b => b.ID == ID).PropertyID == a.PropertyID) != null)
                {
                    Rate oldRate = _c.Rates.Single(a => a.ID == ID);
                    oldRate.ClosedToArrival = EdittedRate.ClosedToArrival;
                    oldRate.DaysAvailable = EdittedRate.DaysAvailable;
                    oldRate.EndDate = EdittedRate.EndDate;
                    oldRate.Formula = EdittedRate.Formula;
                    oldRate.Name = EdittedRate.Name;
                    oldRate.ParentRateID = EdittedRate.ParentRateID;
                    oldRate.RoomsAvailableon = EdittedRate.RoomsAvailableon;
                    oldRate.StartDate = EdittedRate.StartDate;
                    oldRate.StopSellTime = EdittedRate.StopSellTime;
                    _c.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Error = ex;
                return false;
            }
        }

        #endregion

        #region Prices

        public List<Price> GetPricesForDates(int RateID, DateTime StartDate, int NOD)
        {
            try
            {
                //Is Rate Owned by logged in user?
                if (_c.User_Properties.SingleOrDefault(a => a.User_ID == _userID && _c.Rates.Single(b => b.ID == RateID).PropertyID == a.PropertyID) != null)
                {
                    return _c.Prices.Where(a => a.RateID == RateID && (a.Date >= StartDate && a.Date <= StartDate.AddDays(NOD))).ToList();
                }
                return null;
            }
            catch (Exception ex)
            {
                Error = ex;
                return null;
            }

        }

        public bool EditPriceList(int RateID, DateTime StartDate, int NOD, List<Price> PriceList)
        {
            try
            {
                if (_c.User_Properties.SingleOrDefault(a => a.User_ID == _userID && _c.Rates.Single(b => b.ID == RateID).PropertyID == a.PropertyID) != null)
                {
                    _c.Prices.Where(a => a.RateID == RateID && (a.Date >= StartDate && a.Date <= StartDate.AddDays(NOD))).ToList().ForEach(b => UpdateSinglePrice(ref b, PriceList.Single(c => c.ID == b.ID)));
                    _c.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void UpdateSinglePrice(ref Price OldPrice, Price NewPrice)
        {
            OldPrice.MinimumStay = NewPrice.MinimumStay;
            OldPrice.Price1 = NewPrice.Price1;
            OldPrice.StopSell = NewPrice.StopSell;

        }

        public bool AddPrices(int RateID, DateTime StartDate, int NOD, int RoomTypeID)
        {
            if (_c.User_Properties.SingleOrDefault(a => a.User_ID == _userID && _c.Rates.Single(b => b.ID == RateID).PropertyID == a.PropertyID) != null)
            {
                if (_c.User_Properties.SingleOrDefault(a => a.User_ID == _userID && _c.Roomtypes.Single(b => b.ID == RoomTypeID).PropertyID == a.PropertyID) != null)
                {
                    List<Price> NewPrices = new List<Price>();
                    for (int i = 0; i < NOD; i++)
                    {
                        NewPrices.Add(new Price() { Date = DateTime.Now.AddDays(i), MinimumStay = 1, Price1 = 0.00M, RateID = RateID, RoomTypeID = RoomTypeID, StopSell = false });
                    }

                    try
                    {
                        _c.Prices.InsertAllOnSubmit(NewPrices);
                        _c.SubmitChanges();
                        return true;
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
            return false;
        }

        public bool CreateDefaultPricesOnRoom(int newRoomTypeID)
        {

            //Get a list of rates for this user
            List<Rate> rateList = _c.Properties.SingleOrDefault(a => a.Roomtypes.Single(b => b.ID == newRoomTypeID) != null).Rates.ToList();
            foreach (Rate item in rateList)
            {

            
            List<Price> NewPrices = new List<Price>();
            for (int i = 0; i < 365; i++)
            {
                NewPrices.Add(new Price() { Date = DateTime.Now.AddDays(i), MinimumStay = 1, Price1 = 0.00M, RateID = item.ID, RoomTypeID = newRoomTypeID, StopSell = false });
            }

            try
            {
                _c.Prices.InsertAllOnSubmit(NewPrices);
                _c.SubmitChanges();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
            }


            return false;
        }
        #endregion

        #region Availability

        public bool ChangeSingleAvailability(int PropertyID, int RoomTypeID, int NewAvailabilityValue, DateTime Date)
        {
            try
            {
                if (_c.Roomtypes.Single(a => a.ID == RoomTypeID).PropertyID == PropertyID)
                {
                    Availability oldAvail = _c.Availabilities.Single(a => a.RoomtypeID == RoomTypeID && a.Date == Date);
                    oldAvail.Availability1 = (short)NewAvailabilityValue;
                    _c.SubmitChanges();
                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {
                Error = ex;
                return false;
            }
        }

        public bool ChangeMultipleAvailabilities(int PropertyID, int RoomTypeID, List<Models.Availability> NewAvailabilityValues)
        {
            try
            {
                if (_c.Roomtypes.Single(a => a.ID == RoomTypeID).PropertyID == PropertyID)
                {
                    DateTime StartDate = NewAvailabilityValues.First().Date;
                    DateTime EndDate = NewAvailabilityValues.Last().Date;
                    List<Availability> AvailList = _c.Availabilities.Where(a => a.RoomtypeID == RoomTypeID && (a.Date >= StartDate && a.Date <= EndDate)).ToList();

                    foreach (var item in AvailList)
                    {
                        item.Availability1 = (short)NewAvailabilityValues.Single(a => a.Date == item.Date).Availability1;
                    }

                    _c.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Error = ex;
                return false;
            }
        }

        public List<Availability> GetAvailabilities(int PropertyID, List<Roomtype> RoomTypeIDs, DateTime StartDate, DateTime EndDate)
        {
            List<Availability> returnAvail = new List<Availability>();
            try
            {
                for (int i = 0; i < RoomTypeIDs.Count; i++)
                {
                if (_c.Roomtypes.Any(a => a.PropertyID == PropertyID && a.ID == RoomTypeIDs[i].ID))
                {
                        returnAvail.AddRange(_c.Availabilities.Where(a => a.Roomtype.PropertyID == PropertyID && (a.Date >= StartDate && a.Date <= EndDate) && a.RoomtypeID == RoomTypeIDs[i].ID).ToList());
                }
                }
                return returnAvail;
            }
            catch (Exception ex)
            {
                Error = ex;
                return null;
            }
        }

        public Dictionary<int, List<Availability>> GetAllAvailabilities(DateTime StartDate, DateTime EndDate)
        {
            try
            {
                return _c.Roomtypes.Where(a => a.PropertyID == this.PID).ToDictionary(b => b.ID, c => c.Availabilities.Where(a => a.Date >= StartDate && a.Date <= EndDate).ToList());
            }
            catch (Exception ex)
            {

                Error = ex;
                return null;
            }
        }

        public bool CreateDefaultAvailability(Roomtype roomTypeItem)
        {
            List<Availability> AvailList = new List<Availability>();
          
                for (int i = 0; i < 365; i++)
                {
                    AvailList.Add(new Availability() { Availability1 = 0, ClosedToAvailability = 0, RoomtypeID = roomTypeItem.ID, StopSell = false, Date = DateTime.Now.AddDays(i).Date });
                }
            
            try
            {
                _c.Availabilities.InsertAllOnSubmit(AvailList);
                _c.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Exception t = ex;
                return false;
            }
        }

        #endregion

        #region Bookings

        public Booking GetBookingByID(int ID, int PropertyID)
        {
            try
            {
                if (_c.User_Properties.Any(a => a.User_ID == _userID && a.PropertyID == PropertyID))
                {
                    return _c.Bookings.SingleOrDefault(a => a.ID == ID && a.PropertyID == PropertyID);
                }
                return null;
            }
            catch (Exception ex)
            {
                Error = ex;
                return null;
            }
        }

        public List<Booking> GetSubBookings(int MainBookingID, int PropertyID)
        {
            try
            {
                if (_c.User_Properties.Any(a => a.User_ID == _userID && a.PropertyID == PropertyID))
                {
                    return _c.MainBooking_SubBookings.Where(b => b.MainBookingID == MainBookingID && b.Booking1.PropertyID == PropertyID).Select(c => c.Booking1).ToList();
                }
                return null;
            }
            catch (Exception ex)
            {
                Error = ex;
                return null;
            }
        }

        public string AddBookingStepOne(int RoomTypeID, decimal Price, int RatePlanID, DateTime StartDate, DateTime EndDate, int PropertyID)
        {
            string bookingString = Guid.NewGuid().ToString();
            try
            {

                Booking NewBooking = new Booking()
                {
                    StartDate = StartDate,
                    EndDate = EndDate,
                    Nights = (StartDate.Date - EndDate.Date).Days - 1,
                    Price = Price,
                    RateID = RatePlanID,
                    RoomTypeID = RoomTypeID,
                    PropertyID = PropertyID,
                    BookingGuid = bookingString
                };
                _c.Bookings.InsertOnSubmit(NewBooking);
                _c.SubmitChanges();
            }
            catch (Exception ex)
            {
                Error = ex;
                return null;
            }

            return bookingString;
        }
        #endregion

        #region Customers

        public bool AddCustomerToBooking(Customer CustDetails, string BookingID)
        {
            try
            {
                _c.Customers.InsertOnSubmit(CustDetails);
                Booking customersBookings = _c.Bookings.Single(a => a.BookingGuid == BookingID);
                customersBookings.Customer = CustDetails;
                _c.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Error = ex;
                return false;
            }
        }

        public Customer GetCustomerByID(int ID, int PropertyID)
        {
            try
            {
                return _c.Bookings.Where(b => b.PropertyID == PropertyID).Select(b => b.Customer).SingleOrDefault(c => c.ID == ID);
            }
            catch (Exception ex)
            {
                Error = ex;
                return null;
            }

        }

        public List<Customer> GetCustomersForProperty(int PropertyID)
        {
            try
            {
                return _c.Bookings.Where(a => a.PropertyID == PropertyID).Select(b => b.Customer).ToList();
            }
            catch (Exception ex)
            {
                Error = ex;
                return null;
            }
        }
        #endregion

        #region Session

        public bool SaveCurrentPID(int currentPID)
        {
            try
            {
                if (_c.Sessions.SingleOrDefault(a => a.UserID == _userID) == null)
                {
                    //No Session Stored in DB
                    _c.Sessions.InsertOnSubmit(new Session() { CurrentPID = currentPID, SessionID = HttpContext.Current.Session.SessionID, UserID = _userID });
                }
                else if (_c.Sessions.Single(a => a.UserID == _userID).SessionID != HttpContext.Current.Session.SessionID)
                {
                    //Session has changed, update DB
                    Session CurrentSession = _c.Sessions.Single(a => a.UserID == _userID);
                    CurrentSession.SessionID = HttpContext.Current.Session.SessionID;
                    CurrentSession.CurrentPID = currentPID;
                }
                else
                {
                    Session CurrentSession = _c.Sessions.Single(a => a.UserID == _userID);
                    CurrentSession.CurrentPID = currentPID;
                }

                _c.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {

                Error = ex;
                return false;
            }

        }

        public bool RestoreSession()
        {
            try
            {
                if (_c.Sessions.SingleOrDefault(a => a.UserID == _userID) != null)
                {
                    Session newSession = _c.Sessions.Single(a => a.UserID == _userID);
                    newSession.SessionID = HttpContext.Current.Session.SessionID;
                    _currentProperty = newSession.CurrentPID;
                }
                else
                {
                    int PID = _c.User_Properties.Where(a => a.User_ID == _userID).First().PropertyID;
                    SaveCurrentPID(_c.User_Properties.Where(a => a.User_ID == _userID).First().PropertyID);
                    _currentProperty = PID;
                }
                return true;
            }
            catch (Exception ex)
            {
                Error = ex;
                return false;
            }
        }


        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    _userID = "";
                    _propertyID = 0;
                    Error = null;
                    _c.Dispose();

                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~MyBookingDAL() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion





    }
}