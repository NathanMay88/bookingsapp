using MyBookingsApp.Areas.App.Models;
using MyBookingsApp.Areas.App.Models.BookingModule;
using MyBookingsApp.Areas.App.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBookingsApp.Areas.App.Controllers
{
    public class BookingsController : Controller
    {
        [HttpGet]
        public ActionResult Book(DateTime? sd = null, int non = 1, int noa = 1, int noc = 0, int pid = -1)
        {
            if (pid == -1)
            {
                return RedirectToAction("NoID");
            }
            if (sd == null)
            {
                sd = DateTime.Now;
            }

            IBookingModel FullAvailability = new FullAvailabilityBookingModel(pid, sd.Value.Date, sd.Value.AddDays(non).Date, "Own Website", noa,noc);

            return View(FullAvailability);
        }

        [HttpPost]
        public ActionResult UpdateBookableProducts(FullAvailabilityBookingModel currentModel)
        {
            return RedirectToAction("Book", new { sd = currentModel.Options.StartDate.Date.ToString("yyyy-MM-dd"), non = currentModel.Options.NumberOfNights, noa = currentModel.Options.NumberOfAdults, noc = currentModel.Options.NumberOfChildren, pid = currentModel.PID });
        }


        [HttpPost]
        public ActionResult Book(BookingStepOneViewModel Model)
        {
            if (ModelState.IsValid != true)
            {
                return RedirectToAction("Book", new { sd = Model.Options.StartDate, non = Model.Options.NumberOfNights, noa = Model.Options.NumberOfAdults, noc = Model.Options.NumberOfChildren, pid = Model.PID });
            }
            using (DataAccess.MyBookingDAL _c = new DataAccess.MyBookingDAL())
            {
                Model.RoomList.Select(a => a.Selected == true);

                //Select All Rooms that have been selected.
                foreach (BookingRoomList item in Model.RoomList.Where(a => a.Selected == true).ToList())
                {
                    //Select All Prices that have been Selected by user
                    foreach (var innerItem in item.AvailableRates.Where(b=>b.Selected == true))
                    {
                        _c.AddBookingStepOne(item.RoomID, (decimal)innerItem.Price, innerItem.RateID, Model.Options.StartDate, Model.Options.StartDate.AddDays(Model.Options.NumberOfNights), _c.CurrentProperty,Model.BookingID);
                    }
                }
            }

            return RedirectToAction("CustomerDetails", new { bookid = Model.BookingID });
        }

        public ActionResult CustomerDetails(string bookID)
        {
            
            return View();
        }

        public ActionResult FinaliseBooking()
        {
            return View();
        }

        public ActionResult NoID()
        {

            return View();
        }

        // GET: App/Bookings
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        // GET: App/Bookings/Details/5
        [Authorize]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: App/Bookings/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: App/Bookings/Create
        [Authorize]
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: App/Bookings/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: App/Bookings/Edit/5
        [Authorize]
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: App/Bookings/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: App/Bookings/Delete/5
        [HttpPost]
        [Authorize]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
