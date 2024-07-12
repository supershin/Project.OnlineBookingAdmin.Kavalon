using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Project.Booking.Admin.Data.Models
{
    [Index(nameof(BookingNumber), Name = "NonClusteredIndex-20210722-171912", IsUnique = true)]
    public partial class ts_Booking
    {
        public ts_Booking()
        {
            ts_Payment = new HashSet<ts_Payment>();
            ts_Unitbooking_History = new HashSet<ts_Unitbooking_History>();
        }

        [Key]
        public Guid ID { get; set; }
        public Guid? ProjectID { get; set; }
        public Guid? UnitID { get; set; }
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
        public string CustomerCitizenID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? BookingDate { get; set; }
        public int? BookingStatusID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PaymentDueDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PaymentOverDueDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CancelDate { get; set; }
        public bool? FlagActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateDate { get; set; }
        public Guid? CreateBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdateDate { get; set; }
        public Guid? UpdateBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UserUpdateDate { get; set; }
        public Guid? UserUpdateByID { get; set; }

        [ForeignKey(nameof(BookingStatusID))]
        [InverseProperty(nameof(tm_BookingStatus.ts_Booking))]
        public virtual tm_BookingStatus BookingStatus { get; set; }
        [ForeignKey(nameof(CustomerID))]
        [InverseProperty(nameof(tm_Register.ts_Booking))]
        public virtual tm_Register Customer { get; set; }
        [ForeignKey(nameof(ProjectID))]
        [InverseProperty(nameof(tm_Project.ts_Booking))]
        public virtual tm_Project Project { get; set; }
        [ForeignKey(nameof(UnitID))]
        [InverseProperty(nameof(tm_Unit.ts_Booking))]
        public virtual tm_Unit Unit { get; set; }
        [ForeignKey(nameof(UserUpdateByID))]
        [InverseProperty(nameof(tm_User.ts_Booking))]
        public virtual tm_User UserUpdateBy { get; set; }
        [InverseProperty("Booking")]
        public virtual ICollection<ts_Payment> ts_Payment { get; set; }
        [InverseProperty("Booking")]
        public virtual ICollection<ts_Unitbooking_History> ts_Unitbooking_History { get; set; }
    }
}
