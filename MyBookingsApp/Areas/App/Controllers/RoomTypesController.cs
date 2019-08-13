using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBookingsApp.Areas.App.Controllers
{
    [Authorize]
    public class RoomTypesController : Controller
    {
        // GET: App/RoomTypes
        public ActionResult Index()
        {
            using (DataAccess.MyBookingDAL _c = new DataAccess.MyBookingDAL())
            {
                return View(Models.ViewModelTranslator.ToVMList(_c.GetListOfRoomTypes(_c.CurrentProperty)));
            }
        }

        // GET: App/RoomTypes/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        public ActionResult ReloadRoomTypes()
        {
            using (DataAccess.MyBookingDAL _c = new DataAccess.MyBookingDAL())
            {
                return PartialView("_ReloadRoomTypes",Models.ViewModelTranslator.ToVMList(_c.GetListOfRoomTypes(_c.CurrentProperty)));
            }
        }

        // POST: App/RoomTypes/Create
        [HttpPost]
        public ActionResult Create(Models.Management.RoomTypeViewModel Model)
        {
            // TODO: Add insert logic here
            using (DataAccess.MyBookingDAL _c = new DataAccess.MyBookingDAL())
            {
                Models.Roomtype newRoomType = Models.ViewModelTranslator.FromVM(Model);
                newRoomType.PropertyID = _c.CurrentProperty;
                if (_c.AddRoomType(newRoomType))
                {
                    return Json(true);
                }
                ModelState.AddModelError(_c.Error.Message, _c.Error);
            }

            return Json(false);

        }

        // GET: App/RoomTypes/Edit/5
        public ActionResult Edit(int id)
        {
            using (DataAccess.MyBookingDAL _c = new DataAccess.MyBookingDAL())
            {
                Models.Management.RoomTypeViewModel VM = Models.ViewModelTranslator.ToVM(_c.GetSingleRoomTypeByID(id));
                VM.RoomOptions = _c.GetRoomOptionsForRoom(id);
                return PartialView(VM);
            }

        }

        // POST: App/RoomTypes/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Models.Management.RoomTypeViewModel Model)
        {
            using (DataAccess.MyBookingDAL _c = new DataAccess.MyBookingDAL())
            {
                Models.Roomtype newRoomType = Models.ViewModelTranslator.FromVM(Model);
                if (_c.EditRoomType(newRoomType, id))
                {
                    _c.AddRoomOptionsToRoom(Model.RoomOptions.Where(a => a.Selected == true).ToList(), Model.ID);
                    return Json(true);
                }
                ModelState.AddModelError(_c.Error.Message, _c.Error);
            }

            return Json(false);
        }

        // POST: App/RoomTypes/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (DataAccess.MyBookingDAL _c = new DataAccess.MyBookingDAL())
            {
                if (_c.RemoveRoomTypeByID(id))
                {
                    return Json(true);
                }
                ModelState.AddModelError(_c.Error.Message, _c.Error);
            }
            return Json(false);
        }

      
    }
}
