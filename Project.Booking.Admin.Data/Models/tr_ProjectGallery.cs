using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Project.Booking.Admin.Data.Models
{
    public partial class tr_ProjectGallery
    {
        public tr_ProjectGallery()
        {
            tr_ProjectGalleryResource = new HashSet<tr_ProjectGalleryResource>();
        }

        [Key]
        public int ID { get; set; }
        public Guid? ProjectID { get; set; }
        [StringLength(200)]
        public string Name { get; set; }
        public int? LineOrder { get; set; }
        public bool? FlagActie { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateDate { get; set; }
        public Guid? CreateBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdateDate { get; set; }
        public Guid? UpdateBy { get; set; }

        [ForeignKey(nameof(ProjectID))]
        [InverseProperty(nameof(tm_Project.tr_ProjectGallery))]
        public virtual tm_Project Project { get; set; }
        [InverseProperty("ProjectGallery")]
        public virtual ICollection<tr_ProjectGalleryResource> tr_ProjectGalleryResource { get; set; }
    }
}
