using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBookingsApp.Areas.App.Models.Management
{
    public class ImageViewModel
    {
        int ID { get; set; }
        ImageType Type { get; set; }
        string URL { get; set; }
        string Alt { get; set; }
        string Title { get; set; }
    }
    public enum ImageType
    {
        RoomType,
        Property,
        RatePlan
    }
}