using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Project.Booking.Admin.Data.Models
{
    public partial class tm_UnitStatus
    {
        public tm_UnitStatus()
        {
            tm_Unit = new HashSet<tm_Unit>();
            ts_Unitbooking_History = new HashSet<ts_Unitbooking_History>();
        }

        [Key]
        public int ID { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Color { get; set; }
        public int? LineOrder { get; set; }
        public bool? FlagActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateDate { get; set; }
        public Guid? CreateBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdateDate { get; set; }
        public Guid? UpdateBy { get; set; }

        [InverseProperty("UnitStatus")]
        public virtual ICollection<tm_Unit> tm_Unit { get; set; }
        [InverseProperty("UnitStatus")]
        public virtual ICollection<ts_Unitbooking_History> ts_Unitbooking_History { get; set; }
    }
}
