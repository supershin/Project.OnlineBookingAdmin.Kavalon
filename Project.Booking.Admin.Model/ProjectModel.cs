using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Booking.Admin.Model
{
    public class ProjectModel
    {
        public int BUID { get; set; }
        public string BUName { get; set; }
        public Guid ProjectID { get; set; }
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
    }
}
