using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Booking.Admin.Model
{
    public class BookingViewModel
    {
        public string ProjectName { get; set; }
        public Guid BookingID { get; set; }        
        public Guid? UnitID { get; set; }
        public string UnitCode { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerMobile { get; set; }
        public string CutizenID { get; set; }
        public string BookingNumber { get; set; }        
        public decimal? BookingAmount { get; set; }
        public string UnitTypeName { get; set; }        
        public decimal? SellingPrice { get; set; }        
        public decimal? SpecialPrice { get; set; }        
        public decimal? Discount { get; set; }                
        public DateTime? BookingDate { get; set; }
        public DateTime? CancelDate { get; set; }
        public DateTime? PaymentDueDate { get; set; }
        public DateTime? OverDue { get; set; }
        public int? BookingStatusID { get; set; }
        public string BookingStatusName { get; set; }
        public string BookingStatusColor { get; set; }
        public DateTime? UserUpdateDate { get; set; }
        public string  UserUpdateByName { get; set; }
        public IEnumerable<PaymentViewModel> Payments { get; set; }
    }
}
