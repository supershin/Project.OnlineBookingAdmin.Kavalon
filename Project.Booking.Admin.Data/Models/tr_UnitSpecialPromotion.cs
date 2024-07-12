using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Project.Booking.Admin.Data.Models
{
    public partial class tr_UnitSpecialPromotion
    {
        [Key]
        public int ID { get; set; }
        public Guid? UnitID { get; set; }
        public int? SpecialPromotionID { get; set; }
        public bool? FlagActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateDate { get; set; }
        public Guid? CreateBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdateDate { get; set; }
        public Guid? UpdateBy { get; set; }

        [ForeignKey(nameof(SpecialPromotionID))]
        [InverseProperty(nameof(tr_ProjectSpecialPromotion.tr_UnitSpecialPromotion))]
        public virtual tr_ProjectSpecialPromotion SpecialPromotion { get; set; }
        [ForeignKey(nameof(UnitID))]
        [InverseProperty(nameof(tm_Unit.tr_UnitSpecialPromotion))]
        public virtual tm_Unit Unit { get; set; }
    }
}
