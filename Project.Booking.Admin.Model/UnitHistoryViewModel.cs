using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Booking.Admin.Model
{
    public class UnitBookingHistoryViewModel
    {
        public Guid? UnitID { get; set; }
        public int? UnitStatusID { get; set; }
        public Guid? BookingID { get; set; }
        public int? BookingStatusID { get; set; }        
        public DateTime? CreateDate { get; set; }
        public Guid? CreateByID { get; set; }
    }
}
