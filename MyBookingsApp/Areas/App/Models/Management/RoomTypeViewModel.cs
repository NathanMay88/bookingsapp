using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using MyBookingsApp.Code;

namespace MyBookingsApp.Areas.App.Models.Management
{
    public class RoomTypeViewModel
    {
        public int ID { get; set; }
        [MaxLength(25,ErrorMessage ="Name is too long")]
        public string Name { get; set; }
        [GreaterThan("MaxPeople")]
        [NonZero]
        public int MinPeople { get; set; }
        public int MaxPeople { get; set; }
        [NonZero]
        public int DefaultAllotment { get; set; }
        [GreaterThan("DefaultAllotment")]
        public int TotalAllotment { get; set; }
        public List<int> RoomNumbers { get; set; }
        [MaxLength(255,ErrorMessage ="The description is too long, please reduce the size")]
        public string Description { get; set; }
        public List<RoomOptionViewModel> RoomOptions { get; set; }
    }

    public class RoomOptionViewModel
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public string IconLocation { get; set; }
        public bool Selected { get; set; }
    }

   
}