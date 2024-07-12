using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Project.Booking.Admin.Data.Models
{
    public partial class ts_Unitbooking_History
    {
        [Key]
        public int ID { get; set; }
        public Guid? UnitID { get; set; }
        public int? UnitStatusID { get; set; }
        public Guid? BookingID { get; set; }
        public int? BookingStatusID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateDate { get; set; }
        public Guid? CreateByID { get; set; }

        [ForeignKey(nameof(BookingID))]
        [InverseProperty(nameof(ts_Booking.ts_Unitbooking_History))]
        public virtual ts_Booking Booking { get; set; }
        [ForeignKey(nameof(BookingStatusID))]
        [InverseProperty(nameof(tm_BookingStatus.ts_Unitbooking_History))]
        public virtual tm_BookingStatus BookingStatus { get; set; }
        [ForeignKey(nameof(UnitID))]
        [InverseProperty(nameof(tm_Unit.ts_Unitbooking_History))]
        public virtual tm_Unit Unit { get; set; }
        [ForeignKey(nameof(UnitStatusID))]
        [InverseProperty(nameof(tm_UnitStatus.ts_Unitbooking_History))]
        public virtual tm_UnitStatus UnitStatus { get; set; }
    }
}
