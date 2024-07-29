using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Project.Booking.Admin.Data.Models
{
    [Keyless]
    public partial class temp_reward
    {
        [StringLength(255)]
        public string unit { get; set; }
        [StringLength(255)]
        public string Name { get; set; }
        [StringLength(255)]
        public string Email { get; set; }
        [StringLength(50)]
        public string Password { get; set; }
        [StringLength(255)]
        public string remark1 { get; set; }
    }
}
