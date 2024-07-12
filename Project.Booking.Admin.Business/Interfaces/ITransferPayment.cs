using Project.Booking.Admin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Booking.Admin.Business.Interfaces
{
    public interface ITransferPayment
    {
        dynamic GetPaymentList(DTParamModel param, TransferPayment criteria);
        TransferPayment GetPayment(Guid ID);
        void SavePayment(TransferPayment model);
    }
}
