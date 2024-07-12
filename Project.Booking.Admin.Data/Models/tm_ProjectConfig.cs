using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Project.Booking.Admin.Data.Models
{
    public partial class tm_ProjectConfig
    {
        [Key]
        public int ID { get; set; }
        public Guid? ProjectID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CountDownDate { get; set; }
        public string CountDownTitle { get; set; }

        [ForeignKey(nameof(ProjectID))]
        [InverseProperty(nameof(tm_Project.tm_ProjectConfig))]
        public virtual tm_Project Project { get; set; }
    }
}
