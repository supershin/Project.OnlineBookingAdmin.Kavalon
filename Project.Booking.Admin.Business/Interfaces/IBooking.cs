using Project.Booking.Admin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Booking.Admin.Business.Interfaces
{
    public interface IBooking
    {
        IEnumerable<BookingViewModel> GetBookingByProjectAll(Guid projectID);
        BookingViewModel GetBookingByID(Guid bookingID);
        void SaveCancelBooking(BookingViewModel model);
    }
}
