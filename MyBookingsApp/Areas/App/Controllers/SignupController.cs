using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBookingsApp.Areas.App.Models;
using Microsoft.AspNet.Identity;

namespace MyBookingsApp.Areas.App.Controllers
{
    public class SignupController : Controller
    {
        // GET: Signup
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Signup()
        {
            SignupViewModel currentDetails = new SignupViewModel();
            currentDetails.RateDetails.RateList = new List<SignupRateViewModel>();
            currentDetails.RoomTypeDetails.RoomtypeList = new List<SignupRoomTypeViewModel>();
            Session.Add("currentDetails", currentDetails);
            return View(currentDetails);
        }

        [HttpPost]
        public ActionResult Signup(bool save)
        {
            SignupViewModel model = ((Models.SignupViewModel)Session["currentDetails"]);
            if (ModelState.IsValid != false)
            {

                //Save Model to Database
                //Save Company First
                using (MyBookingsApp.Areas.App.DataAccess.MyBookingDAL _c = new DataAccess.MyBookingDAL())
                {
                    int CompanyID = _c.AddCompany(ViewModelTranslator.FromVM(model.CompanySignupModel));
                    Property newProp = ViewModelTranslator.FromVM(model.PropertyDetails);
                    newProp.CompanyID = CompanyID;
                    _c.AddProperty(newProp);

                    foreach (var item in model.RoomTypeDetails.RoomtypeList)
                    {
                        Roomtype localRoom = ViewModelTranslator.FromVM(item);
                        localRoom.PropertyID = _c.PID;
                        _c.AddRoomType(localRoom);
                    }

                    foreach (var item in model.RateDetails.RateList)
                    {
                        Rate LocalRate = ViewModelTranslator.FromVM(item);
                        LocalRate.PropertyID = _c.PID;
                        _c.AddRate(LocalRate);
                    }
                    Session.Add("CurrentProperty", _c.PID);
                }
                //Save Property
                //Save Rooms
                //Save Rates
                //Setup Dummy Prices and Availability
                Session.Remove("currentDetails");
                
                return RedirectToAction("Index", "Dashboard", null);
            }
            else
            {
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult SaveCompanyDetails(Models.SignupStepOneViewModel model)
        {
            if (ModelState.IsValid != false)
            {
                ((Models.SignupViewModel)Session["currentDetails"]).CompanySignupModel = model;
                return PartialView("_CompanyCreateAjax", model);
            }
            ModelState.AddModelError("", "Please Review your submission and try again");
            return PartialView("_CompanyCreateAjax", model);
        }

        [HttpPost]
        public ActionResult SavePropertyDetails(Models.SignupStepTwoViewModel model)
        {
            if (ModelState.IsValid != false)
            {
                ((Models.SignupViewModel)Session["currentDetails"]).PropertyDetails = model;
                return PartialView("_PropertyCreateAjax", model);
            }
            ModelState.AddModelError("", "Please Review your submission and try again");
            return PartialView("_PropertyCreateAjax", model);
        }


        [HttpGet]
        public ActionResult RoomTypeListGet()
        {
            return PartialView("_RoomtypeListPartial");
        }

        [HttpPost]
        public ActionResult RoomTypeListPost(Models.SignupRoomTypeViewModel model)
        {
            List<Models.SignupRoomTypeViewModel> currentRoomList = ((Models.SignupViewModel)Session["currentDetails"]).RoomTypeDetails.RoomtypeList;
            if (ModelState.IsValid != false)
            {
                model.TempID = Guid.NewGuid().ToString();
                currentRoomList.Add(model);
                ((Models.SignupViewModel)Session["currentDetails"]).RoomTypeDetails = new SignupStepThreeViewModel() { RoomtypeList = currentRoomList };

            }
            return PartialView("_RoomtypeListAjaxPartial", currentRoomList);
        }

        [HttpPost]
        public ActionResult DeleteRoomType(string TempID)
        {
            List<SignupRoomTypeViewModel> currentRoomList = ((Models.SignupViewModel)Session["currentDetails"]).RoomTypeDetails.RoomtypeList;
            if (currentRoomList.Any(a => a.TempID == TempID))
            {
                currentRoomList.Remove(currentRoomList.Single(a => a.TempID == TempID));
            }
            return PartialView("_RoomtypeListAjaxPartial", currentRoomList);
        }

        [HttpGet]
        public ActionResult CreateRate()
        {

            return PartialView("_RateCreatePartial");
        }

        [HttpPost]
        public ActionResult CreateRatePost(Models.SignupRateViewModel model)
        {
            List<Models.SignupRateViewModel> currentRateList = ((Models.SignupViewModel)Session["currentDetails"]).RateDetails.RateList;
            if (ModelState.IsValid != false)
            {
                model.TempID = Guid.NewGuid().ToString();
                if (currentRateList == null)
                {
                    currentRateList = new List<Models.SignupRateViewModel>
                    {
                        model
                    };
                    ((Models.SignupViewModel)Session["currentDetails"]).RateDetails = new SignupStepFourViewModel() { RateList = currentRateList };
                }
                else
                {
                    currentRateList.Add(model);
                    ((Models.SignupViewModel)Session["currentDetails"]).RateDetails.RateList = currentRateList;
                }
            }
            return PartialView("_RateListAjaxPartial", currentRateList);
        }

        [HttpPost]
        public ActionResult DeleteRate(string TempID)
        {
            List<SignupRateViewModel> currentRateList = ((Models.SignupViewModel)Session["currentDetails"]).RateDetails.RateList;
            if (currentRateList.Any(a => a.TempID == TempID))
            {
                currentRateList.Remove(currentRateList.Single(a => a.TempID == TempID));
            }
            return PartialView("_RateListAjaxPartial", currentRateList);
        }
    }
}