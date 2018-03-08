using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBookingsApp.Areas.App.Models
{
    static class ViewModelTranslator
    {

        #region From VM
        public static Company FromVM(SignupStepOneViewModel VM)
        {
            Company NewComp = new Company
            {
                Address1 = VM.CompanyAddress1,
                Address2 = VM.CompanyAddress2,
                Address3 = VM.CompanyAddress3,
                Address4 = VM.CompanyAddress4,
                Country = VM.CompanyCountry,
                Email = VM.CompanyEmail,
                Phone = VM.CompanyPhone,
                Name = VM.CompanyName
            };
            return NewComp;
        }

        public static Property FromVM(SignupStepTwoViewModel VM)
        {
            Property newProp = new Property
            {
                Address1 = VM.PropertyAddress1,
                Address2 = VM.PropertyAddress2,
                Address3 = VM.PropertyAddress3,
                Address4 = VM.PropertyAddress4,
                Country = VM.PropertyCountry,
                Email = VM.PropertyEmail,
                Name = VM.PropertyName,
                Phone = VM.PropertyPhone
            };
            return newProp;
        }

        public static Roomtype FromVM(SignupRoomTypeViewModel VM)
        {
            return new Roomtype
            {
                DefaultAllotment = (short)VM.DefaultAllotment,
                MaxPeople = (short)VM.MaxPeople,
                MinPeople = (short)VM.MinPeople,
                Name = VM.RoomTypeName,
                MinPrice = (decimal)VM.MinPrice
            };
        }

        public static Rate FromVM(SignupRateViewModel VM)
        {
            Rate newRate = new Rate
            {
                DaysAvailable = string.Join(",", VM.SelectedDays.Where(a => a.Selected == true).Select(b => b.Value)),
                Name = VM.RateName,
                ClosedToArrival = VM.ClosedToArrival,
                StopSellTime = TimeSpan.Parse(VM.StopSellTime.ToString("hh:mm:ss")),
                StartDate = DateTime.Now.Date,
                EndDate = DateTime.Now.Date.AddDays(1)
            };
            return newRate;

        }

        public static Roomtype FromVM(Models.Management.RoomTypeViewModel VM)
        {
            Roomtype newRoom = new Roomtype
            {
                DefaultAllotment = (short)VM.DefaultAllotment,
                MaxPeople = (short)VM.MaxPeople,
                MinPeople = (short)VM.MinPeople,
                Name = VM.Name,
                ActualAllotment = (short)VM.TotalAllotment,
                Description = VM.Description
            };
            return newRoom;
        }

        public static Rate FromVM(Models.Management.RatePlanViewModel VM)
        {
            Rate newRate = new Rate
            {
                ClosedToArrival = VM.ClosedToArrival,
                EndDate = VM.EndDate,
                Name = VM.Name,
                ParentRateID = VM.ParentRateID,
                StartDate = VM.StartDate,
                DaysAvailable = string.Join(",", VM.AvailableDays.Where(a => a.Selected == true).Select(b => b.Value))
            };
            return newRate;
        }

        public static List<Availability> FromVM(Models.Management.AvailabilityGroupedViewModel VM)
        {
            List<Availability> newAvail = VM.AvailabilityList.Select(a => new Availability()
            {
                Availability1 = (short)a.Availability,
                ClosedToAvailability = (short)a.ClosedToAvailability,
                Date = a.Date,
                RoomtypeID = VM.RoomTypeID,
                StopSell = a.StopSell
            }).ToList();

            return newAvail;
        }

        #endregion

        #region To VM

        public static Models.Management.RoomTypeViewModel ToVM(Models.Roomtype DBModel)
        {
            Models.Management.RoomTypeViewModel VM = new Management.RoomTypeViewModel()
            {
                DefaultAllotment = (int)DBModel.DefaultAllotment,
                Description = DBModel.Description,
                ID = DBModel.ID,
                MaxPeople = (int)DBModel.MaxPeople,
                MinPeople = (int)DBModel.MinPeople,
                TotalAllotment = (DBModel.ActualAllotment != null) ? (int)DBModel.ActualAllotment : 0,
                Name = DBModel.Name

            };
            return VM;
        }

        public static Models.Management.RatePlanViewModel ToVM(Models.Rate DBModel)
        {
            Models.Management.RatePlanViewModel VM = new Management.RatePlanViewModel()
            {
                ID = DBModel.ID,
                Name = DBModel.Name,
                ClosedToArrival = DBModel.ClosedToArrival,
                EndDate = DBModel.EndDate,
                StartDate = DBModel.StartDate,
                ParentRateID = DBModel.ParentRateID == null ? 0 : (int)DBModel.ParentRateID,

            };

            List<string> availableDays = DBModel.DaysAvailable.Split(',').ToList();
            for (int i = 0; i < availableDays.Count; i++)
            {
                bool result = int.TryParse(availableDays[i], out int val);
                if (result)
                {
                    VM.AvailableDays[val].Selected = true;
                }
            }
            return VM;
        }

        public static List<Models.Management.RoomTypeViewModel> ToVMList(List<Models.Roomtype> DBModel)
        {
            List<Models.Management.RoomTypeViewModel> response = new List<Management.RoomTypeViewModel>();
            foreach (var item in DBModel)
            {
                response.Add(new Management.RoomTypeViewModel()
                {
                    DefaultAllotment = (int)item.DefaultAllotment,
                    ID = item.ID,
                    MaxPeople = (int)item.MaxPeople,
                    MinPeople = (int)item.MinPeople,
                    Name = item.Name
                });

            }
            return response;
        }

        public static List<Models.Management.RatePlanViewModel> ToVMList(List<Models.Rate> DBModel)
        {
            List<Models.Management.RatePlanViewModel> response = new List<Management.RatePlanViewModel>();
            foreach (var item in DBModel)
            {
                Management.RatePlanViewModel VMRate = new Management.RatePlanViewModel()
                {
                    ID = item.ID,
                    Name = item.Name,
                    ClosedToArrival = item.ClosedToArrival,
                    EndDate = item.EndDate,
                    StartDate = item.StartDate,
                    ParentRateID = item.ParentRateID == null ? 0 : (int)item.ParentRateID

                };

                List<string> availableDays = item.DaysAvailable.Split(',').ToList();
                for (int i = 0; i < availableDays.Count; i++)
                {
                    bool result = int.TryParse(availableDays[i], out int val);
                    if (result)
                    {
                        VMRate.AvailableDays[val].Selected = true;
                    }

                }
                response.Add(VMRate);
            }
            return response;
        }

        public static Models.Management.AvailabilityListViewModel ToVM(List<Models.Availability> DBModel, List<Roomtype> RoomTypeList)
        {
            Management.AvailabilityListViewModel VMAvailability = new Management.AvailabilityListViewModel()
            {
                GroupedAvailabilityList = new List<Management.AvailabilityGroupedViewModel>()
            };


            foreach (var outitem in DBModel.GroupBy(a => a.RoomtypeID, b => b))
            {
                Management.AvailabilityGroupedViewModel availList = new Management.AvailabilityGroupedViewModel()
                {
                    RoomTypeID = outitem.Key,
                    RoomTypeName = RoomTypeList.Where(a => a.ID == outitem.Key).Select(b => b.Name).First(),
                    AvailabilityList = new List<Management.AvailabilityItemViewModel>()
                };
                foreach (var item in outitem)
                {
                    Management.AvailabilityItemViewModel AvailItem = new Management.AvailabilityItemViewModel()
                    {
                        Availability = (short)item.Availability1,
                        ClosedToAvailability = (short)item.ClosedToAvailability,
                        Date = item.Date,
                        StopSell = item.StopSell

                    };
                    availList.AvailabilityList.Add(AvailItem);        
                }
                VMAvailability.GroupedAvailabilityList.Add(availList);
            }
            
            VMAvailability.Options = new Management.AvailabilityOptionsViewModel()
            {
                EndDate = DBModel.Last().Date,
                StartDate = DBModel.First().Date,
                SelectedRoomTypeID = DBModel.First().RoomtypeID,
                SelectedRoomTypeName = DBModel.First().Roomtype.Name,
                RoomTypeList = RoomTypeList.Select(a => new System.Web.Mvc.SelectListItem() { Text = a.Name, Value = a.ID.ToString(), Selected = (a.ID == DBModel.First().RoomtypeID) ? true : false }).ToList()
            };
            return VMAvailability;
        }

        #endregion

    }
}