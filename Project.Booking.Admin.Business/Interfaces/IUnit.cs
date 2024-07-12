using Project.Booking.Admin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Booking.Admin.Business.Interfaces
{
    public interface IUnit
    {
        IEnumerable<UnitViewModel> GetUnitByProjectAll(Guid projectID);
        UnitViewModel GetUnitByID(Guid unitID);
        void SaveUnit(UnitViewModel model);
        void SaveUnitBookingHistory(Guid unitID, int unitStatusID, Guid? bookingID = null, int? bookingStatusID = null);
        void UpdateUnitStatus(Guid unitID, int unitStatusID);
        MatrixView GetUnitMatrix(Guid projectID, string builds);
        string GetBuilds(Guid projectID);
    }
}
