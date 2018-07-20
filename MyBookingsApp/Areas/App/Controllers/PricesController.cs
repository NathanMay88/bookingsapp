using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBookingsApp.Areas.App.Models.Management;

namespace MyBookingsApp.Areas.App.Controllers
{
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
                
                return View(Models.ViewModelTranslator.ToVM(PriceList, RateList.First().ID, RateList));
            }
            
        }

        [HttpPost]
        public ActionResult Index(PriceListViewModel model)
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
                    List<Models.Rate> RateList = _c.GetListOfRates(_c.CurrentProperty);
                    List<Models.Price> PriceList = _c.GetPricesForDates(RateList.First().ID, DateTime.Now.Date, 30).ToList();
                    return View(Models.ViewModelTranslator.ToVM(PriceList, RateList.First().ID, RateList));
                

            }
            
        }
    }
}
