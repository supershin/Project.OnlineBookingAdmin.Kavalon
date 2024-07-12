using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Project.Booking.Admin.Data.Models
{
    public partial class tm_Unit
    {
        public tm_Unit()
        {
            tr_UnitSpecialPromotion = new HashSet<tr_UnitSpecialPromotion>();
            tr_UnitVIP = new HashSet<tr_UnitVIP>();
            ts_Booking = new HashSet<ts_Booking>();
            ts_Unitbooking_History = new HashSet<ts_Unitbooking_History>();
        }

        [Key]
        public Guid ID { get; set; }
        public Guid? ProjectID { get; set; }
        [StringLength(20)]
        public string UnitCode { get; set; }
        public int? BuildID { get; set; }
        public int? FloorID { get; set; }
        public int? Room { get; set; }
        public int? UnitTypeID { get; set; }
        public int? ModelTypeID { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Area { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? AreaIncrease { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? SellingPrice { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? SpecialPrice { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Discount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? BookingAmount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? ContractAmount { get; set; }
        public int? UnitStatusID { get; set; }
        public Guid? AnnotationID { get; set; }
        public bool? FlagActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateDate { get; set; }
        public Guid? CreateBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdateDate { get; set; }
        public Guid? UpdateBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UserUpdateDate { get; set; }
        public Guid? UserUpdateByID { get; set; }

        [ForeignKey(nameof(AnnotationID))]
        [InverseProperty(nameof(tm_Annotation.tm_Unit))]
        public virtual tm_Annotation Annotation { get; set; }
        [ForeignKey(nameof(BuildID))]
        [InverseProperty(nameof(tm_Build.tm_Unit))]
        public virtual tm_Build Build { get; set; }
        [ForeignKey(nameof(FloorID))]
        [InverseProperty(nameof(tm_Floor.tm_Unit))]
        public virtual tm_Floor Floor { get; set; }
        [ForeignKey(nameof(ModelTypeID))]
        [InverseProperty(nameof(tm_ModelType.tm_Unit))]
        public virtual tm_ModelType ModelType { get; set; }
        [ForeignKey(nameof(ProjectID))]
        [InverseProperty(nameof(tm_Project.tm_Unit))]
        public virtual tm_Project Project { get; set; }
        [ForeignKey(nameof(UnitStatusID))]
        [InverseProperty(nameof(tm_UnitStatus.tm_Unit))]
        public virtual tm_UnitStatus UnitStatus { get; set; }
        [ForeignKey(nameof(UnitTypeID))]
        [InverseProperty(nameof(tm_UnitType.tm_Unit))]
        public virtual tm_UnitType UnitType { get; set; }
        [ForeignKey(nameof(UserUpdateByID))]
        [InverseProperty(nameof(tm_User.tm_Unit))]
        public virtual tm_User UserUpdateBy { get; set; }
        [InverseProperty("Unit")]
        public virtual tr_UnitReserve tr_UnitReserve { get; set; }
        [InverseProperty("Unit")]
        public virtual ICollection<tr_UnitSpecialPromotion> tr_UnitSpecialPromotion { get; set; }
        [InverseProperty("Unit")]
        public virtual ICollection<tr_UnitVIP> tr_UnitVIP { get; set; }
        [InverseProperty("Unit")]
        public virtual ICollection<ts_Booking> ts_Booking { get; set; }
        [InverseProperty("Unit")]
        public virtual ICollection<ts_Unitbooking_History> ts_Unitbooking_History { get; set; }
    }
}
