using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Booking.Admin.Model
{
    public class SummaryUnit
    {
        public int Total { get; set; }
        public int Close { get; set; }
        public int Available { get; set; }
        public int Booking { get; set; }        
        public int Payment { get; set; }
    }
}
