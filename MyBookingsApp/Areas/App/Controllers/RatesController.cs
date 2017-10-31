using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBookingsApp.Areas.App.Controllers
{
    public class RatesController : Controller
    {
        // GET: App/Rates
        public ActionResult Index()
        {
            using (DataAccess.MyBookingDAL _c = new DataAccess.MyBookingDAL())
            {
                return View(Models.ViewModelTranslator.ToVMList(_c.GetListOfRates(_c.CurrentProperty)));
            }
        }

        public ActionResult ReloadRates()
        {
            using (DataAccess.MyBookingDAL _c = new DataAccess.MyBookingDAL())
            {
                return PartialView("_ReloadRates", Models.ViewModelTranslator.ToVMList(_c.GetListOfRates(_c.CurrentProperty)));
            }
        }

        // GET: App/Rates/Create
        public ActionResult Create()
        {
            return PartialView(new MyBookingsApp.Areas.App.Models.Management.RatePlanViewModel());
        }

        // POST: App/Rates/Create
        [HttpPost]
        public ActionResult Create(Models.Management.RatePlanViewModel Model)
        {
            // TODO: Add insert logic here
            using (DataAccess.MyBookingDAL _c = new DataAccess.MyBookingDAL())
            {
                Models.Rate newRate= Models.ViewModelTranslator.FromVM(Model);
                newRate.PropertyID = _c.CurrentProperty;
                if (_c.AddRate(newRate))
                {
                    return Json(true);
                }
                ModelState.AddModelError(_c.Error.Message, _c.Error);
            }

            return Json(false);

        }

        // GET: App/Rates/Edit/5
        public ActionResult Edit(int id)
        {
            using (DataAccess.MyBookingDAL _c = new DataAccess.MyBookingDAL())
            {
                return PartialView(Models.ViewModelTranslator.ToVM(_c.GetSingleRateByID(id)));
            }
        }

        // POST: App/Rates/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Models.Management.RatePlanViewModel Model)
        {
            using (DataAccess.MyBookingDAL _c = new DataAccess.MyBookingDAL())
            {
                Models.Rate newRate = Models.ViewModelTranslator.FromVM(Model);
                if (_c.EditRate(newRate, id))
                {
                    return Json(true);
                }
                ModelState.AddModelError(_c.Error.Message, _c.Error);
            }
            return Json(false);
        }

        // POST: App/Rates/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (DataAccess.MyBookingDAL _c = new DataAccess.MyBookingDAL())
            {
                if (_c.DeleteRateByID(id))
                {
                    return Json(true);
                }
                ModelState.AddModelError(_c.Error.Message, _c.Error);
            }
            return Json(false);
        }
    }
}
