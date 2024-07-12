using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Project.Booking.Admin.Data.Models
{
    public partial class tm_Department
    {
        public tm_Department()
        {
            tm_User = new HashSet<tm_User>();
            tr_DepartmentBU_Mapping = new HashSet<tr_DepartmentBU_Mapping>();
        }

        [Key]
        public int ID { get; set; }
        [StringLength(50)]
        public string Code { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public int? LineOrder { get; set; }
        public bool? FlagActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateDate { get; set; }
        public int? CreateBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdateDate { get; set; }
        public int? UpdateBy { get; set; }

        [InverseProperty("Department")]
        public virtual ICollection<tm_User> tm_User { get; set; }
        [InverseProperty("Department")]
        public virtual ICollection<tr_DepartmentBU_Mapping> tr_DepartmentBU_Mapping { get; set; }
    }
}
