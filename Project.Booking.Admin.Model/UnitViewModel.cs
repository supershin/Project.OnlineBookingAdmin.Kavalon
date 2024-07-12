using Project.Booking.Admin.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Project.Booking.Admin.Model
{
    public class UnitViewModel 
    {        
        public Guid? ID { get; set; }        
        public Guid? UnitID { get; set; }
        public string ProjectName { get; set; }
        public string UnitCode { get; set; }                          
        public decimal? Area { get; set; }
        public decimal? AreaIncrease { get; set; }
        [Required]
        [Range(typeof(decimal),"0","999999999999")]        
        public decimal? SellingPrice { get; set; }
        [Required]
        [Range(typeof(decimal), "0", "999999999999")]        
        public decimal? SpecialPrice { get; set; }
        [Required]
        [Range(typeof(decimal), "0", "999999999999")]        
        public decimal? Discount { get; set; }
        [Required]
        [Range(typeof(decimal), "0", "999999999999")]        
        public decimal? BookingAmount { get; set; }
       
        public DateTime? UserUpdateDate { get; set; }
        public string UserUpdateByName { get; set; }
        public string UnitTypeName { get; set; }
        public string BuildName { get; set; }
        public string FloorName { get; set; }
        public string ModelTypeName { get; set; }
        [Required]
        public int? UnitStatusID { get; set; }
        public string UnitStatusName { get; set; }
        public string UnitStatusColor { get; set; }
        public bool isCancelBooking { get; set; }
    }
}
