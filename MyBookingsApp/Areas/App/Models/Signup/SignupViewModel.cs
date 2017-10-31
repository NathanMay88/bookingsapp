using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MyBookingsApp.Areas.App.Models
{
    public class SignupStepOneViewModel
    {
        //Company Details for Signup
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string CompanyAddress1 { get; set; }
        [Required]
        public string CompanyAddress2 { get; set; }
        public string CompanyAddress3 { get; set; }
        public string CompanyAddress4 { get; set; }
        [Required]
        public string CompanyCountry { get; set; }
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please enter a valid Email Address")]
        public string CompanyEmail { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Please enter a valid Phone Number")]
        public string CompanyPhone { get; set; }
    }

    public class SignupStepTwoViewModel
    {
        //Property Details for Signup
        [Required]
        public string PropertyName { get; set; }
        [Required]
        public string PropertyAddress1 { get; set; }
        [Required]
        public string PropertyAddress2 { get; set; }
        public string PropertyAddress3 { get; set; }
        public string PropertyAddress4 { get; set; }
        [Required]
        public string PropertyCountry { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage = "Please enter a valid Email Address")]
        [Required()]
        public string PropertyEmail { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Please enter a valid Phone Number")]
        public string PropertyPhone { get; set; }
    }

    public class SignupStepThreeViewModel
    {
        [MyBookingsApp.Code.MustHaveOneElement(ErrorMessage = "You must enter at least 1 Room Type")]
        public List<SignupRoomTypeViewModel> RoomtypeList { get; set; }
    }

    public class SignupStepFourViewModel
    {
        [MyBookingsApp.Code.MustHaveOneElement(ErrorMessage = "You must enter at least 1 Rate")]
        public List<SignupRateViewModel> RateList { get; set; }
    }
    public class SignupViewModel
    {
        private SignupStepOneViewModel _companySignupModel;
        public SignupStepOneViewModel CompanySignupModel
        {
            get
            {
                if (_companySignupModel == null)
                {
                    _companySignupModel = new SignupStepOneViewModel();
                }
                return _companySignupModel;
            }
            set
            {
                _companySignupModel = value;
            }
        }
        private SignupStepTwoViewModel _propertyDetails;
        public SignupStepTwoViewModel PropertyDetails
        {
            get
            {
                if (_propertyDetails == null)
                { _propertyDetails = new SignupStepTwoViewModel(); }
                return _propertyDetails;
            }
            set
            { _propertyDetails = value; }
        }
        private SignupStepThreeViewModel _roomTypeDetails;
        public SignupStepThreeViewModel RoomTypeDetails
        {
            get
            {
                if (_roomTypeDetails == null) { _roomTypeDetails = new SignupStepThreeViewModel(); }
                return _roomTypeDetails;
            }
            set { _roomTypeDetails = value; }
        }
        private SignupStepFourViewModel _rateDetails;
        public SignupStepFourViewModel RateDetails
        {
            get
            {
                if (_rateDetails == null)
                { _rateDetails = new SignupStepFourViewModel(); }
                return _rateDetails;
            }
            set { _rateDetails = value; }
        }
    }

    public class SignupRoomTypeViewModel
    {
        public string RoomTypeName { get; set; }
        public int MinPeople { get; set; }
        public int MaxPeople { get; set; }
        public float MinPrice { get; set; }
        public int DefaultAllotment { get; set; }
        public string TempID { get; set; }
    }

    public class SignupRateViewModel
    {
        public string RateName { get; set; }
        [DataType(DataType.Time)]
        public DateTime StopSellTime { get; set; }
        public int ClosedToArrival { get; set; }
        public string TempID { get; set; }

        private List<SelectListItem> _selectedDays;
        public List<SelectListItem> SelectedDays
        {
            get
            {
                if (_selectedDays == null)
                {
                    _selectedDays = new List<SelectListItem>();
                    for (int i = 0; i <= 6; i++)
                    {
                        _selectedDays.Add(new SelectListItem() { Selected = false, Text = ((DayOfWeek)(i == 6 ? 0 : i + 1)).ToString(), Value = (i == 6 ? 0 : i + 1).ToString() });
                    }
                    return _selectedDays;
                }
                else { return _selectedDays; }
            }
            set
            {
                _selectedDays = value;
            }
        }

    }
}