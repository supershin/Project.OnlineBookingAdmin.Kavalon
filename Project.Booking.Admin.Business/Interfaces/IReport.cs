using Project.Booking.Admin.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Booking.Admin.Business.Interfaces
{
    public interface IReport
    {
        DataTable GetUnitData(ReportViewModel criteria);
        DataTable GetBookingPaymentData(ReportViewModel criteria);
        DataTable GetRegisterData(ReportViewModel criteria);
        DataTable GetTransferPaymentData(ReportViewModel criteria);
    }
}
