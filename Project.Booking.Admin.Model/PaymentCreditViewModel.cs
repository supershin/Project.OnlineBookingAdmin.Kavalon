using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Booking.Admin.Model
{
    public class PaymentCreditViewModel
    {       
        public string CardName { get; set; }
        public string CardBank { get; set; }                
        public string CardLastDigit { get; set; }        
        public string CardBrand { get; set; }
        public long? Amount { get; set; }
        public string Status { get; set; }
        public string FailureMessage { get; set; }
        public string PaymentDesciption { get {
                return $"{this.CardName} ,{this.CardBank} ,{this.CardBrand} ,***{this.CardLastDigit}";
            } }
        
    }
}
