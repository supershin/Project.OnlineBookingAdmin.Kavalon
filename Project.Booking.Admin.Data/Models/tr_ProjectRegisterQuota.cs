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
        [Key]
        public int ID { get; set; }
        public Guid? ProjectID { get; set; }
        public Guid? RegisterID { get; set; }
        public int? Quota { get; set; }
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
        [ForeignKey(nameof(RegisterID))]
        [InverseProperty(nameof(tm_Register.tr_ProjectRegisterQuota))]
        public virtual tm_Register Register { get; set; }
    }
}
