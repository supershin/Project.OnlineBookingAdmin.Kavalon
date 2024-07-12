using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Project.Booking.Admin.Data.Models
{
    public partial class tm_Ext
    {
        public tm_Ext()
        {
            tm_Register = new HashSet<tm_Register>();
            tr_ProjectResource = new HashSet<tr_ProjectResource>();
            ts_Payment = new HashSet<ts_Payment>();
        }

        [Key]
        public int ID { get; set; }
        public int? ExtTypeID { get; set; }
        [StringLength(2000)]
        public string Name { get; set; }
        public int? LineOrder { get; set; }
        public bool? FlagActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateDate { get; set; }
        public Guid? CreateBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdateDate { get; set; }
        public Guid? UpdateBy { get; set; }

        [ForeignKey(nameof(ExtTypeID))]
        [InverseProperty(nameof(tm_ExtType.tm_Ext))]
        public virtual tm_ExtType ExtType { get; set; }
        [InverseProperty("RegisterType")]
        public virtual ICollection<tm_Register> tm_Register { get; set; }
        [InverseProperty("ResourceType")]
        public virtual ICollection<tr_ProjectResource> tr_ProjectResource { get; set; }
        [InverseProperty("PaymentType")]
        public virtual ICollection<ts_Payment> ts_Payment { get; set; }
    }
}
