using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Booking.Admin.Model
{
    public class PaymentViewModel
    {
        public string PaymentNo { get; set; }
        public string PaymentTypeName { get; set; }
        public DateTime? PaymentDate { get; set; }

        public PaymentCreditViewModel PaymentCredit { get; set; }
    }
}
