using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Project.Booking.Admin.Data.Models
{
    public partial class tr_ProjectModelType
    {
        [Key]
        public int ID { get; set; }
        public Guid? ProjectID { get; set; }
        public int? ModelTypeID { get; set; }
        public Guid? ResourceID { get; set; }
        public bool? FlagActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateDate { get; set; }
        public Guid? CreateBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdateDate { get; set; }
        public Guid? UpdateBy { get; set; }

        [ForeignKey(nameof(ModelTypeID))]
        [InverseProperty(nameof(tm_ModelType.tr_ProjectModelType))]
        public virtual tm_ModelType ModelType { get; set; }
        [ForeignKey(nameof(ProjectID))]
        [InverseProperty(nameof(tm_Project.tr_ProjectModelType))]
        public virtual tm_Project Project { get; set; }
        [ForeignKey(nameof(ResourceID))]
        [InverseProperty(nameof(tm_Resource.tr_ProjectModelType))]
        public virtual tm_Resource Resource { get; set; }
    }
}
