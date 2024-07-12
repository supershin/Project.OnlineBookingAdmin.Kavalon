using Project.Booking.Admin.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Booking.Admin.Model
{
    public class ProjectRegisterQuota : tr_ProjectRegisterQuota
    {
        public string ProjectName { get; set; }
        public int? UseQuota { get; set; }
    }
}
