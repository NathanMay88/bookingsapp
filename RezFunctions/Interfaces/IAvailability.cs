using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezFunctions.Interfaces
{
    interface IAvailability
    {
        /// <summary>
        /// Reduce a Single Dates Availability by 1 for a single Room on a single Property
        /// </summary>
        /// <param name="roomID"></param>
        /// <param name="propertyID"></param>
        /// <param name="date"></param>
        void ReduceAvailability(int roomID, int propertyID, DateTime date);
        /// <summary>
        /// Increase a Single Dates Availability by 1 for a single room on a single Property
        /// </summary>
        /// <param name="roomID"></param>
        /// <param name="propertyID"></param>
        /// <param name="date"></param>
        void IncreaseAvailability(int roomID, int propertyID, DateTime date);
        
        /// <summary>
        /// Gets a Set of Availability for selected rooms against one property
        /// </summary>
        /// <param name="roomIDs"></param>
        /// <param name="propertyID">Used to check if room is attached to property</param>
        /// <param name="startDate"></param>
        /// <param name="numberofNights"></param>
        void GetAvailability(List<int> roomIDs, int propertyID, DateTime startDate, int numberofNights);
        /// <summary>
        /// List of Availability that is currently being worked on
        /// </summary>
        IEnumerable<IAvailabilityListItem> AvailabilityList { get; set; }
        /// <summary>
        /// Room ID of the currently Loaded Availability
        /// </summary>
        int RoomID { get; set; }

    }

    interface IAvailabilityListItem{

        DateTime Startdate { get; set; }

        int RoomID { get; set; }

        IEnumerable<int> AvailabilityValues { get; set; }
    }
}
