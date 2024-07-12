using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Project.Booking.Admin.Data.Models
{
    public partial class tm_Project
    {
        public tm_Project()
        {
            tm_ProjectConfig = new HashSet<tm_ProjectConfig>();
            tm_Unit = new HashSet<tm_Unit>();
            tr_Matrix_Config = new HashSet<tr_Matrix_Config>();
            tr_ProjectBuild = new HashSet<tr_ProjectBuild>();
            tr_ProjectBuildFloor = new HashSet<tr_ProjectBuildFloor>();
            tr_ProjectBuildGroup = new HashSet<tr_ProjectBuildGroup>();
            tr_ProjectGallery = new HashSet<tr_ProjectGallery>();
            tr_ProjectModelType = new HashSet<tr_ProjectModelType>();
            tr_ProjectRegisterQuota = new HashSet<tr_ProjectRegisterQuota>();
            tr_ProjectResource = new HashSet<tr_ProjectResource>();
            tr_ProjectSpecialPromotion = new HashSet<tr_ProjectSpecialPromotion>();
            tr_ProjectTransferPayment = new HashSet<tr_ProjectTransferPayment>();
            ts_Booking = new HashSet<ts_Booking>();
            ts_Order = new HashSet<ts_Order>();
        }

        [Key]
        public Guid ID { get; set; }
        public int? CompanyID { get; set; }
        public int? BUID { get; set; }
        [StringLength(20)]
        public string ProjectCode { get; set; }
        [StringLength(2000)]
        public string ProjectNameTH { get; set; }
        [StringLength(2000)]
        public string ProjectNameEN { get; set; }
        [StringLength(1000)]
        public string Location { get; set; }
        [StringLength(2000)]
        public string Vocation { get; set; }
        public int? LineOrder { get; set; }
        public bool? FlagActive { get; set; }
        public bool? IsOnlineBooking { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateDate { get; set; }
        public Guid? CreateBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdateDate { get; set; }
        public Guid? UpdateBy { get; set; }

        [ForeignKey(nameof(BUID))]
        [InverseProperty(nameof(tm_BU.tm_Project))]
        public virtual tm_BU BU { get; set; }
        [ForeignKey(nameof(CompanyID))]
        [InverseProperty(nameof(tm_Company.tm_Project))]
        public virtual tm_Company Company { get; set; }
        [InverseProperty("Project")]
        public virtual ICollection<tm_ProjectConfig> tm_ProjectConfig { get; set; }
        [InverseProperty("Project")]
        public virtual ICollection<tm_Unit> tm_Unit { get; set; }
        [InverseProperty("Project")]
        public virtual ICollection<tr_Matrix_Config> tr_Matrix_Config { get; set; }
        [InverseProperty("Project")]
        public virtual ICollection<tr_ProjectBuild> tr_ProjectBuild { get; set; }
        [InverseProperty("Project")]
        public virtual ICollection<tr_ProjectBuildFloor> tr_ProjectBuildFloor { get; set; }
        [InverseProperty("Project")]
        public virtual ICollection<tr_ProjectBuildGroup> tr_ProjectBuildGroup { get; set; }
        [InverseProperty("Project")]
        public virtual ICollection<tr_ProjectGallery> tr_ProjectGallery { get; set; }
        [InverseProperty("Project")]
        public virtual ICollection<tr_ProjectModelType> tr_ProjectModelType { get; set; }
        [InverseProperty("Project")]
        public virtual ICollection<tr_ProjectRegisterQuota> tr_ProjectRegisterQuota { get; set; }
        [InverseProperty("Project")]
        public virtual ICollection<tr_ProjectResource> tr_ProjectResource { get; set; }
        [InverseProperty("Project")]
        public virtual ICollection<tr_ProjectSpecialPromotion> tr_ProjectSpecialPromotion { get; set; }
        [InverseProperty("Project")]
        public virtual ICollection<tr_ProjectTransferPayment> tr_ProjectTransferPayment { get; set; }
        [InverseProperty("Project")]
        public virtual ICollection<ts_Booking> ts_Booking { get; set; }
        [InverseProperty("Project")]
        public virtual ICollection<ts_Order> ts_Order { get; set; }
    }
}
