using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Project.Booking.Admin.Data.Models
{
    public partial class ts_Payment_Credit
    {
        [Key]
        public Guid ID { get; set; }
        public Guid? PaymentID { get; set; }
        [StringLength(100)]
        public string ChargeID { get; set; }
        [StringLength(50)]
        public string IP { get; set; }
        public string Customer { get; set; }
        public long? Refunded { get; set; }
        public long? Amount { get; set; }
        [StringLength(50)]
        public string Currency { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        [StringLength(50)]
        public string Status { get; set; }
        [StringLength(50)]
        public string FailureCode { get; set; }
        public string FailureMessage { get; set; }
        public string OmiseTransaction { get; set; }
        public bool? Paid { get; set; }
        public bool? Reversed { get; set; }
        public bool? Authorized { get; set; }
        public bool? Capture { get; set; }
        public string ReturnURI { get; set; }
        [StringLength(50)]
        public string CardFinancing { get; set; }
        [StringLength(500)]
        public string CardBank { get; set; }
        [StringLength(50)]
        public string CardFirstDigit { get; set; }
        [StringLength(50)]
        public string CardLastDigit { get; set; }
        [StringLength(50)]
        public string CardBrand { get; set; }
        public int? CardExpirationMonth { get; set; }
        public int? CardExpirationYear { get; set; }
        [StringLength(200)]
        public string CardName { get; set; }
        public string AuthorizeURI { get; set; }
        public bool? FlagActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateDate { get; set; }
        public Guid? CreateBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdateDate { get; set; }
        public Guid? UpdateBy { get; set; }

        [ForeignKey(nameof(PaymentID))]
        [InverseProperty(nameof(ts_Payment.ts_Payment_Credit))]
        public virtual ts_Payment Payment { get; set; }
    }
}
