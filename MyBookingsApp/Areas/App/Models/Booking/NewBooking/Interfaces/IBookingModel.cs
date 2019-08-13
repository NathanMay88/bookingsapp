using System;
using System.Collections.Generic;

namespace MyBookingsApp.Areas.App.Models.Interface
{
    public interface IBookingModel
    {
        List<IAvailableRoomList> GetListOfAvailableProducts(int PropertyID, DateTime StartDate, DateTime EndDate, int NumberofAdults, int NumberofChildren);
    }
}