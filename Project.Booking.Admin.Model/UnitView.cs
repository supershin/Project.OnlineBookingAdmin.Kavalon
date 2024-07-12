using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Booking.Admin.Model
{
    public class UnitView
    {
        public Guid ID { get; set; }
        public Guid ProjectID { get; set; }
        public string UnitCode { get; set; }
        public int BuildID { get; set; }
        public string Build { get; set; }
        public int Floor { get; set; }
        public string FloorName { get; set; }
        public int Room { get; set; }
        public int UnitStatusID { get; set; }
        public string UnitStatusName { get; set; }
        public string UnitStatusColor { get; set; }
    }
}
