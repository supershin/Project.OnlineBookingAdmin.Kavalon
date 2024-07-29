using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Project.Booking.Admin.Data.Models
{
    public partial class ts_Booking_VIP
    {
        [Key]
        public Guid ID { get; set; }
        public Guid? ProjectID { get; set; }
        public Guid? UnitID { get; set; }
        public Guid? BookingID { get; set; }
        [StringLength(50)]
        public string BookingNumber { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? BookingAmount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? ContractAmount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? SellingPrice { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? SpecialPrice { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Discount { get; set; }
        public Guid? CustomerID { get; set; }
        [StringLength(500)]
        public string CustomerFirstName { get; set; }
        [StringLength(500)]
        public string CustomerLastName { get; set; }
        [StringLength(100)]
        public string CustomerEmail { get; set; }
        [StringLength(100)]
        public string CustomerMobile { get; set; }
        [StringLength(50)]
        public string CustomerCitizen { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? BookingDate { get; set; }
        public int BookingStatusID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PaymentDueDate { get; set; }
        public int? PaymentOverDueDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CancelDate { get; set; }
        public bool FlagActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreateDate { get; set; }
        public Guid CreateBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime UpdateDate { get; set; }
        public Guid UpdateBy { get; set; }
        public bool? IsSendMail { get; set; }
        [StringLength(50)]
        public string UnitCode { get; set; }
        [StringLength(200)]
        public string Email { get; set; }
    }
}
