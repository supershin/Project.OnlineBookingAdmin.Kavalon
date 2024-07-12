using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Project.Booking.Admin.Data.Models
{
    public partial class tm_Build
    {
        public tm_Build()
        {
            tm_Unit = new HashSet<tm_Unit>();
            tr_Matrix_Config = new HashSet<tr_Matrix_Config>();
            tr_ProjectBuild = new HashSet<tr_ProjectBuild>();
            tr_ProjectBuildFloor = new HashSet<tr_ProjectBuildFloor>();
            tr_ProjectBuildGroupBuild = new HashSet<tr_ProjectBuildGroup>();
            tr_ProjectBuildGroupParentBuild = new HashSet<tr_ProjectBuildGroup>();
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
        public DateTime? UpdateDate { get; set; }
        public Guid? UpdateBy { get; set; }

        [InverseProperty("Build")]
        public virtual ICollection<tm_Unit> tm_Unit { get; set; }
        [InverseProperty("Build")]
        public virtual ICollection<tr_Matrix_Config> tr_Matrix_Config { get; set; }
        [InverseProperty("Build")]
        public virtual ICollection<tr_ProjectBuild> tr_ProjectBuild { get; set; }
        [InverseProperty("Build")]
        public virtual ICollection<tr_ProjectBuildFloor> tr_ProjectBuildFloor { get; set; }
        [InverseProperty(nameof(tr_ProjectBuildGroup.Build))]
        public virtual ICollection<tr_ProjectBuildGroup> tr_ProjectBuildGroupBuild { get; set; }
        [InverseProperty(nameof(tr_ProjectBuildGroup.ParentBuild))]
        public virtual ICollection<tr_ProjectBuildGroup> tr_ProjectBuildGroupParentBuild { get; set; }
    }
}
