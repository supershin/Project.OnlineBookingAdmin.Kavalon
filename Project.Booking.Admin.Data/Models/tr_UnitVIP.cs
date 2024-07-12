using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Project.Booking.Admin.Data.Models
{
    public partial class tr_UnitVIP
    {
        [Key]
        public int ID { get; set; }
        public Guid? UnitID { get; set; }
        [StringLength(3000)]
        public string BookName { get; set; }
        public bool? FlagActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateDate { get; set; }
        public Guid? CreateBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdateDate { get; set; }
        public Guid? UpdateBy { get; set; }

        [ForeignKey(nameof(UnitID))]
        [InverseProperty(nameof(tm_Unit.tr_UnitVIP))]
        public virtual tm_Unit Unit { get; set; }
    }
}
