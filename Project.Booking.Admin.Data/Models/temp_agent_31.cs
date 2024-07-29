using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Project.Booking.Admin.Data.Models
{
    [Keyless]
    public partial class temp_agent_31
    {
        public int? Id { get; set; }
        [StringLength(255)]
        public string Unit { get; set; }
        [StringLength(255)]
        public string Name { get; set; }
        [StringLength(255)]
        public string Email { get; set; }
        [StringLength(255)]
        public string Password { get; set; }
        [StringLength(255)]
        public string Remark { get; set; }
    }
}
