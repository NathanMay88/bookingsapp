using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBookingsApp.Areas.App.Models.Management;

namespace MyBookingsApp.Areas.App.Controllers
{
    [Authorize]
    public class PricesController : Controller
    {
        [HttpGet]
        // GET: App/Prices
        public ActionResult Index()
        {
            PriceListViewModel model = new PriceListViewModel();
            using (DataAccess.MyBookingDAL _c = new DataAccess.MyBookingDAL())
            {
                List<Models.Rate> RateList = _c.GetListOfRates(_c.CurrentProperty);
                if (RateList.Count == 0)
                {
                    RedirectToAction("Index", "Rate");
                }
                List<Models.Price> PriceList = _c.GetPricesForDates(RateList.First().ID, DateTime.Now.Date, 30).ToList();
                List<Models.Roomtype> RoomTypeList = _c.GetListOfRoomTypes(_c.CurrentProperty);
                return View(Models.ViewModelTranslator.ToVM(PriceList, RateList.First().ID, RateList, RoomTypeList));
            }

        }

        [HttpPost]
        public ActionResult Update(PriceListViewModel model)
        {
            using (DataAccess.MyBookingDAL _c = new DataAccess.MyBookingDAL())
            {
                if (ModelState.IsValid == true)
                {
                    foreach (var item in model.GroupedPriceList)
                    {
                        _c.ChangeMultiplePrices(_c.GetSingleRoomTypeByID(item.RoomTypeID).PropertyID, item.RateID, item.RoomTypeID, Models.ViewModelTranslator.FromVM(item));

                    }
                }
                
                
                // Needed to make sure the values are correct on the page
                ModelState.Clear();
                List<Models.Rate> RateList = _c.GetListOfRates(_c.CurrentProperty);
                List<Models.Price> PriceList = _c.GetPricesForDates(model.SelectedRateID, model.PriceOptions.StartDate, (model.PriceOptions.EndDate - model.PriceOptions.StartDate).Days).ToList();
                List<Models.Roomtype> RoomTypeList = _c.GetListOfRoomTypes(_c.CurrentProperty);
                PriceListViewModel vm = Models.ViewModelTranslator.ToVM(PriceList, model.SelectedRateID, RateList, RoomTypeList);
                vm.SelectedRateID = model.SelectedRateID;
                vm.PriceOptions.StartDate = model.PriceOptions.StartDate;
                vm.PriceOptions.EndDate = model.PriceOptions.EndDate;
                return View("Index",vm);


            }

        }

        [HttpGet]
        public ActionResult ChangeRoomType(int rt)
        {
            return View();
        }

        [HttpGet]
        public ActionResult ChangeDates(DateTime sd, int non)
        {
            return View();
        }
    }
}
