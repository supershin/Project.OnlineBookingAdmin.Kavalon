using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Project.Booking.Admin.Data.Models
{
    [Keyless]
    public partial class temp_agent_heritage
    {
        [Column("code it")]
        public double? code_it { get; set; }
        public double? id { get; set; }
        [StringLength(255)]
        public string unit { get; set; }
        [StringLength(255)]
        public string name { get; set; }
        [StringLength(255)]
        public string email { get; set; }
        [StringLength(255)]
        public string password { get; set; }
        [Column("Remark 1")]
        [StringLength(255)]
        public string Remark_1 { get; set; }
    }
}
