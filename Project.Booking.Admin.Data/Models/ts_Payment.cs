using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Project.Booking.Admin.Data.Models
{
    public partial class ts_Payment
    {
        public ts_Payment()
        {
            ts_Payment_Credit = new HashSet<ts_Payment_Credit>();
        }

        [Key]
        public Guid ID { get; set; }
        public Guid? BookingID { get; set; }
        public int? PaymentTypeID { get; set; }
        [StringLength(50)]
        public string PaymentNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PaymentDate { get; set; }
        public bool? FlagActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateDate { get; set; }
        public Guid? CreateBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdateDate { get; set; }
        public Guid? UpdateBy { get; set; }

        [ForeignKey(nameof(BookingID))]
        [InverseProperty(nameof(ts_Booking.ts_Payment))]
        public virtual ts_Booking Booking { get; set; }
        [ForeignKey(nameof(PaymentTypeID))]
        [InverseProperty(nameof(tm_Ext.ts_Payment))]
        public virtual tm_Ext PaymentType { get; set; }
        [InverseProperty("Payment")]
        public virtual ICollection<ts_Payment_Credit> ts_Payment_Credit { get; set; }
    }
}
