using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Project.Booking.Admin.Data.Models
{
    public partial class tm_Floor
    {
        public tm_Floor()
        {
            tm_Unit = new HashSet<tm_Unit>();
            tr_ProjectBuildFloor = new HashSet<tr_ProjectBuildFloor>();
        }

        [Key]
        public int ID { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public bool? FlagActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateDate { get; set; }
        public Guid? CreateBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdateDate { get; set; }
        public Guid? UpdateBy { get; set; }

        [InverseProperty("Floor")]
        public virtual ICollection<tm_Unit> tm_Unit { get; set; }
        [InverseProperty("Floor")]
        public virtual ICollection<tr_ProjectBuildFloor> tr_ProjectBuildFloor { get; set; }
    }
}
