using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Project.Booking.Admin.Data.Models
{
    [Index(nameof(Name), Name = "NonClusteredIndex-20210717-123516", IsUnique = true)]
    public partial class tm_ModelType
    {
        public tm_ModelType()
        {
            tm_Unit = new HashSet<tm_Unit>();
            tr_ProjectModelType = new HashSet<tr_ProjectModelType>();
        }

        [Key]
        public int ID { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public bool? FlagActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateDate { get; set; }
        public Guid? CreateBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdateDtae { get; set; }
        public Guid? UpdateBy { get; set; }

        [InverseProperty("ModelType")]
        public virtual ICollection<tm_Unit> tm_Unit { get; set; }
        [InverseProperty("ModelType")]
        public virtual ICollection<tr_ProjectModelType> tr_ProjectModelType { get; set; }
    }
}
