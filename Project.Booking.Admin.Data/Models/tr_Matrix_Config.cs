using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Project.Booking.Admin.Data.Models
{
    public partial class tr_Matrix_Config
    {
        [Key]
        public int ID { get; set; }
        public Guid? ProjectID { get; set; }
        public int? BuildID { get; set; }
        [StringLength(500)]
        public string Text { get; set; }
        public int? RowNo { get; set; }
        public int? ColSpan { get; set; }
        public int? LineOrder { get; set; }
        public string Style { get; set; }
        public bool? FlagActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateDate { get; set; }
        public Guid? CreateBy { get; set; }

        [ForeignKey(nameof(BuildID))]
        [InverseProperty(nameof(tm_Build.tr_Matrix_Config))]
        public virtual tm_Build Build { get; set; }
        [ForeignKey(nameof(ProjectID))]
        [InverseProperty(nameof(tm_Project.tr_Matrix_Config))]
        public virtual tm_Project Project { get; set; }
    }
}
