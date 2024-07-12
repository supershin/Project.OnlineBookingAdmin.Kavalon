using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Project.Booking.Admin.Data.Models
{
    public partial class ts_Order
    {
        [Key]
        public Guid ID { get; set; }
        public Guid? ProjectID { get; set; }
        [StringLength(50)]
        public string OrderNumber { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? OrderDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ExpireDate { get; set; }
        public int? OrderStatusID { get; set; }
        [StringLength(500)]
        public string CustomerFirstName { get; set; }
        [StringLength(500)]
        public string CustomerLastName { get; set; }
        [StringLength(100)]
        public string CustomerEmail { get; set; }
        [StringLength(100)]
        public string CustomerMobile { get; set; }
        [StringLength(1000)]
        public string SaleReferenceEmail { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? TotalSellingPrice { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Discount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? NetPrice { get; set; }
        public bool? FlagActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateDate { get; set; }
        public Guid? CreateBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdateDate { get; set; }
        public Guid? UpdateBy { get; set; }

        [ForeignKey(nameof(OrderStatusID))]
        [InverseProperty(nameof(tm_OrderStatus.ts_Order))]
        public virtual tm_OrderStatus OrderStatus { get; set; }
        [ForeignKey(nameof(ProjectID))]
        [InverseProperty(nameof(tm_Project.ts_Order))]
        public virtual tm_Project Project { get; set; }
    }
}
