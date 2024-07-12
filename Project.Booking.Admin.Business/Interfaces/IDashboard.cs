using Project.Booking.Admin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Booking.Admin.Business.Interfaces
{
    public interface IDashboard
    {
        void GetData(DashboardViewModel model);
    }
}
