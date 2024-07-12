using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Project.Booking.Admin.Data.Models
{
    public partial class tr_ProjectBuildGroup
    {
        [Key]
        public int ID { get; set; }
        public Guid? ProjectID { get; set; }
        public int? BuildID { get; set; }
        public int? ParentBuildID { get; set; }

        [ForeignKey(nameof(BuildID))]
        [InverseProperty(nameof(tm_Build.tr_ProjectBuildGroupBuild))]
        public virtual tm_Build Build { get; set; }
        [ForeignKey(nameof(ParentBuildID))]
        [InverseProperty(nameof(tm_Build.tr_ProjectBuildGroupParentBuild))]
        public virtual tm_Build ParentBuild { get; set; }
        [ForeignKey(nameof(ProjectID))]
        [InverseProperty(nameof(tm_Project.tr_ProjectBuildGroup))]
        public virtual tm_Project Project { get; set; }
    }
}
