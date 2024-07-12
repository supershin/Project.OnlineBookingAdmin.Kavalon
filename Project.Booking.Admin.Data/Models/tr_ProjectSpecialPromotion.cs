using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Project.Booking.Admin.Data.Models
{
    public partial class tr_ProjectSpecialPromotion
    {
        public tr_ProjectSpecialPromotion()
        {
            tr_UnitSpecialPromotion = new HashSet<tr_UnitSpecialPromotion>();
        }

        [Key]
        public int ID { get; set; }
        public Guid? ProjectID { get; set; }
        public string Name { get; set; }
        [Column(TypeName = "date")]
        public DateTime? StartDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? EndDate { get; set; }
        public bool? FlagActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateDate { get; set; }
        public Guid? CreateBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdateDate { get; set; }
        public Guid? UpdateBy { get; set; }

        [ForeignKey(nameof(ProjectID))]
        [InverseProperty(nameof(tm_Project.tr_ProjectSpecialPromotion))]
        public virtual tm_Project Project { get; set; }
        [InverseProperty("SpecialPromotion")]
        public virtual ICollection<tr_UnitSpecialPromotion> tr_UnitSpecialPromotion { get; set; }
    }
}
