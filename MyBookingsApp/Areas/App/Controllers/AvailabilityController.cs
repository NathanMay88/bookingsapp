using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBookingsApp.Areas.App.Controllers
{
    [Authorize]
    public class AvailabilityController : Controller
    {
        // GET: App/Availability
        public ActionResult Index()
        {
            using (DataAccess.MyBookingDAL _c = new DataAccess.MyBookingDAL())
            {
                List<Models.Availability> AvailabilityList = _c.GetAvailabilities(_c.CurrentProperty, _c.GetListOfRoomTypes(_c.CurrentProperty), DateTime.Now.Date, DateTime.Now.AddDays(30).Date).ToList();
                List<Models.Roomtype> RoomTypeList = _c.GetListOfRoomTypes(_c.CurrentProperty);
                return View(Models.ViewModelTranslator.ToVM(AvailabilityList, RoomTypeList));
            }
            
        }

        [HttpPost]
        public ActionResult Index(Models.Management.AvailabilityListViewModel model)
        {
            using (DataAccess.MyBookingDAL _c = new DataAccess.MyBookingDAL())
            {
                if (ModelState.IsValid == true)
                {
                    foreach (var item in model.GroupedAvailabilityList)
                    {
                        _c.ChangeMultipleAvailabilities(_c.GetSingleRoomTypeByID(item.RoomTypeID).PropertyID, item.RoomTypeID,Models.ViewModelTranslator.FromVM(item));
                        
                    }
                }

                List<Models.Availability> AvailabilityList = _c.GetAvailabilities(_c.CurrentProperty, _c.GetListOfRoomTypes(_c.CurrentProperty), model.Options.StartDate, model.Options.EndDate).ToList();
                List<Models.Roomtype> RoomTypeList = _c.GetListOfRoomTypes(_c.CurrentProperty);
                return View(Models.ViewModelTranslator.ToVM(AvailabilityList, RoomTypeList));
            }
        }



    }
}
