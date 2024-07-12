using Project.Booking.Admin.Data.Models;
using Project.Booking.Admin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Booking.Admin.Business.Interfaces
{
    public interface IMaster
    {
        List<tm_Ext> GetMasterExt(int ExtTypeID);
        List<tm_Department> GetMasterDepartment();
        List<tm_Role> GetMasterRole();
        List<ProjectModel> GetMasterProject(int departmentID);
        List<tm_UnitStatus> GetMasterUnitStatus();
        List<tm_BookingStatus> GetMasterBookingStatus();
        List<BUModel> GetMasterBU(int departmentID);
        List<ProjectModel> GetMasterProjectByBU(string buIDs);
    }
}
