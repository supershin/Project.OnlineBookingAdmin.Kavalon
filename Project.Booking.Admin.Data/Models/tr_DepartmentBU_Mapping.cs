using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Project.Booking.Admin.Data.Models
{
    public partial class tr_DepartmentBU_Mapping
    {
        [Key]
        public int ID { get; set; }
        public int? DepartmentID { get; set; }
        public int? BUID { get; set; }

        [ForeignKey(nameof(BUID))]
        [InverseProperty(nameof(tm_BU.tr_DepartmentBU_Mapping))]
        public virtual tm_BU BU { get; set; }
        [ForeignKey(nameof(DepartmentID))]
        [InverseProperty(nameof(tm_Department.tr_DepartmentBU_Mapping))]
        public virtual tm_Department Department { get; set; }
    }
}
