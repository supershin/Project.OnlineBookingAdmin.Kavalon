using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Project.Booking.Admin.Data.Models
{
    public partial class tr_ProjectRegisterQuota
    {
        public tr_ProjectRegisterQuota()
        {
            ts_Payment = new HashSet<ts_Payment>();
        }

        [Key]
        public int ID { get; set; }
        public Guid? ProjectID { get; set; }
        public Guid? RegisterID { get; set; }
        public int? ProjectQuotaID { get; set; }
        public int? Quota { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? TotalPrice { get; set; }
        public int? StatusID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ApproveDate_1 { get; set; }
        public int? ApproveStatusID_1 { get; set; }
        public Guid? ApproveBy_1 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ApproveDate_2 { get; set; }
        public int? ApproveStatusID_2 { get; set; }
        public Guid? ApproveBy_2 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CancelDate { get; set; }
        public Guid? CancelBy { get; set; }
        public string CancelRemark { get; set; }
        public bool? FlagActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateDate { get; set; }
        public Guid? CreateBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdateDate { get; set; }
        public Guid? UpdateBy { get; set; }

        [ForeignKey(nameof(ProjectID))]
        [InverseProperty(nameof(tm_Project.tr_ProjectRegisterQuota))]
        public virtual tm_Project Project { get; set; }
        [ForeignKey(nameof(ProjectQuotaID))]
        [InverseProperty(nameof(tr_ProjectQuota.tr_ProjectRegisterQuota))]
        public virtual tr_ProjectQuota ProjectQuota { get; set; }
        [ForeignKey(nameof(RegisterID))]
        [InverseProperty(nameof(tm_Register.tr_ProjectRegisterQuota))]
        public virtual tm_Register Register { get; set; }
        [InverseProperty("ProjectRegisterQuota")]
        public virtual ICollection<ts_Payment> ts_Payment { get; set; }
    }
}
