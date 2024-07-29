using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Project.Booking.Admin.Data.Models
{
    public partial class tr_ProjectQuota
    {
        public tr_ProjectQuota()
        {
            tr_ProjectRegisterQuota = new HashSet<tr_ProjectRegisterQuota>();
        }

        [Key]
        public int ID { get; set; }
        public Guid? ProjectID { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public int? Quota { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? TotalPrice { get; set; }
        public int? LineOrder { get; set; }
        public bool? FlagActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateDate { get; set; }
        public Guid? CraeteBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdateDate { get; set; }
        public Guid? UpdateBy { get; set; }

        [InverseProperty("ProjectQuota")]
        public virtual ICollection<tr_ProjectRegisterQuota> tr_ProjectRegisterQuota { get; set; }
    }
}
