using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBookingsApp.Areas.App.Models.Interface
{
    public interface IAvailableRoomList
    {
        
        void GetPrices(int RoomID, DateTime StartDate, DateTime EndDate);
        
    }
}
