using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Project.Booking.Admin.Data.Models
{
    public partial class tr_ProjectTransferPayment
    {
        [Key]
        public Guid ID { get; set; }
        public Guid? ProjectID { get; set; }
        public Guid? RegisterID { get; set; }
        public Guid? ResourceID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TransferDate { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Amount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? ApproveAmount { get; set; }
        public int? StatusID { get; set; }
        public Guid? VerifyByID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? VerifyDate { get; set; }
        public int? Quota { get; set; }
        public bool? FlagActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CraeteDate { get; set; }
        public Guid? CreateBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdateDate { get; set; }
        public Guid? UpdateBy { get; set; }

        [ForeignKey(nameof(ProjectID))]
        [InverseProperty(nameof(tm_Project.tr_ProjectTransferPayment))]
        public virtual tm_Project Project { get; set; }
        [ForeignKey(nameof(RegisterID))]
        [InverseProperty(nameof(tm_Register.tr_ProjectTransferPayment))]
        public virtual tm_Register Register { get; set; }
        [ForeignKey(nameof(ResourceID))]
        [InverseProperty(nameof(tm_Resource.tr_ProjectTransferPayment))]
        public virtual tm_Resource Resource { get; set; }
    }
}
