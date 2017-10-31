using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBookingsApp.Areas.App.Models.Management;

namespace MyBookingsApp.Areas.App.Controllers
{
    public class PropertyController : Controller
    {
        // GET: App/Property
        public ActionResult Index(int id)
        {
             
            return View();
        }

        // POST: App/Property/Edit/5
        [HttpPost]
        public ActionResult Index(int id, PropertyViewModel collection)
        {
            try
            {
                

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
