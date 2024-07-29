using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Project.Booking.Admin.Data.Models
{
    public partial class tm_Resource
    {
        public tm_Resource()
        {
            tm_WebImage = new HashSet<tm_WebImage>();
            tr_ProjectBuildFloor = new HashSet<tr_ProjectBuildFloor>();
            tr_ProjectGalleryResource = new HashSet<tr_ProjectGalleryResource>();
            tr_ProjectModelType = new HashSet<tr_ProjectModelType>();
            tr_ProjectResource = new HashSet<tr_ProjectResource>();
            tr_ProjectTransferPayment = new HashSet<tr_ProjectTransferPayment>();
            ts_Payment_Transfer = new HashSet<ts_Payment_Transfer>();
        }

        [Key]
        public Guid ID { get; set; }
        [StringLength(1000)]
        public string FileName { get; set; }
        public string FilePath { get; set; }
        [StringLength(500)]
        public string MimeType { get; set; }
        public bool? IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateDate { get; set; }
        public Guid? CreateBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdateDate { get; set; }
        public Guid? UpdateBy { get; set; }

        [InverseProperty("Resource")]
        public virtual ICollection<tm_WebImage> tm_WebImage { get; set; }
        [InverseProperty("Resource")]
        public virtual ICollection<tr_ProjectBuildFloor> tr_ProjectBuildFloor { get; set; }
        [InverseProperty("Resource")]
        public virtual ICollection<tr_ProjectGalleryResource> tr_ProjectGalleryResource { get; set; }
        [InverseProperty("Resource")]
        public virtual ICollection<tr_ProjectModelType> tr_ProjectModelType { get; set; }
        [InverseProperty("Resource")]
        public virtual ICollection<tr_ProjectResource> tr_ProjectResource { get; set; }
        [InverseProperty("Resource")]
        public virtual ICollection<tr_ProjectTransferPayment> tr_ProjectTransferPayment { get; set; }
        [InverseProperty("Resource")]
        public virtual ICollection<ts_Payment_Transfer> ts_Payment_Transfer { get; set; }
    }
}
