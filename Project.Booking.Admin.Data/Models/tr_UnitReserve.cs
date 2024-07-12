using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Project.Booking.Admin.Data.Models
{
    public partial class tr_UnitReserve
    {
        [Key]
        public Guid UnitID { get; set; }
        [StringLength(200)]
        public string ReserverUserEmail { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ReserveDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ExpireDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateDate { get; set; }
        public Guid? CreateBy { get; set; }

        [ForeignKey(nameof(UnitID))]
        [InverseProperty(nameof(tm_Unit.tr_UnitReserve))]
        public virtual tm_Unit Unit { get; set; }
    }
}
