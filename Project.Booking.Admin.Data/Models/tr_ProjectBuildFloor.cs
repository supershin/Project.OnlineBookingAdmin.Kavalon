using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Project.Booking.Admin.Data.Models
{
    public partial class tr_ProjectBuildFloor
    {
        [Key]
        public int ID { get; set; }
        public Guid? ProjectID { get; set; }
        public int? BuildID { get; set; }
        public int? FloorID { get; set; }
        public Guid? ResourceID { get; set; }
        public int? LineOrder { get; set; }
        public bool? FlagActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateDate { get; set; }
        public Guid? CreateBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdateDate { get; set; }
        public Guid? UpdateBy { get; set; }

        [ForeignKey(nameof(BuildID))]
        [InverseProperty(nameof(tm_Build.tr_ProjectBuildFloor))]
        public virtual tm_Build Build { get; set; }
        [ForeignKey(nameof(FloorID))]
        [InverseProperty(nameof(tm_Floor.tr_ProjectBuildFloor))]
        public virtual tm_Floor Floor { get; set; }
        [ForeignKey(nameof(ProjectID))]
        [InverseProperty(nameof(tm_Project.tr_ProjectBuildFloor))]
        public virtual tm_Project Project { get; set; }
        [ForeignKey(nameof(ResourceID))]
        [InverseProperty(nameof(tm_Resource.tr_ProjectBuildFloor))]
        public virtual tm_Resource Resource { get; set; }
    }
}
