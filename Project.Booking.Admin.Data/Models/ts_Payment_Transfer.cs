using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Project.Booking.Admin.Data.Models
{
    public partial class ts_Payment_Transfer
    {
        [Key]
        public Guid ID { get; set; }
        public Guid? PaymentID { get; set; }
        public Guid? ResourceID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TransferDate { get; set; }
        [StringLength(2)]
        public string Hours { get; set; }
        [StringLength(2)]
        public string Minutes { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Amount { get; set; }
        public bool? FlagActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateDate { get; set; }
        public Guid? CreateBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdateDate { get; set; }
        public Guid? UpdateBy { get; set; }

        [ForeignKey(nameof(ResourceID))]
        [InverseProperty(nameof(tm_Resource.ts_Payment_Transfer))]
        public virtual tm_Resource Resource { get; set; }
    }
}
