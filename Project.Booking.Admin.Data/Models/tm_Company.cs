using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Project.Booking.Admin.Data.Models
{
    public partial class tm_Company
    {
        public tm_Company()
        {
            tm_Project = new HashSet<tm_Project>();
        }

        [Key]
        public int ID { get; set; }
        [StringLength(50)]
        public string CompanyID { get; set; }
        [StringLength(500)]
        public string CompanyName { get; set; }
        [StringLength(100)]
        public string OmisePublicKey { get; set; }
        [StringLength(100)]
        public string OmiseSecurityKey { get; set; }
        public bool? FlagActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateDate { get; set; }
        public int? CreateBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdateDate { get; set; }
        public int? UpdateBy { get; set; }

        [InverseProperty("Company")]
        public virtual ICollection<tm_Project> tm_Project { get; set; }
    }
}
