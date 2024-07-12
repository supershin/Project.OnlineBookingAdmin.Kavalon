using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Project.Booking.Admin.Data.Models
{
    public partial class tr_ProjectGalleryResource
    {
        [Key]
        public int ID { get; set; }
        public int? ProjectGalleryID { get; set; }
        public Guid? ResourceID { get; set; }
        public int? LineOrder { get; set; }
        public bool? FlagActie { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateDate { get; set; }
        public Guid? CreateBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdateDate { get; set; }
        public Guid? UpdateBy { get; set; }

        [ForeignKey(nameof(ProjectGalleryID))]
        [InverseProperty(nameof(tr_ProjectGallery.tr_ProjectGalleryResource))]
        public virtual tr_ProjectGallery ProjectGallery { get; set; }
        [ForeignKey(nameof(ResourceID))]
        [InverseProperty(nameof(tm_Resource.tr_ProjectGalleryResource))]
        public virtual tm_Resource Resource { get; set; }
    }
}
