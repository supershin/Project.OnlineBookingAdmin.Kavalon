using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Project.Booking.Admin.Data.Models
{
    [Keyless]
    public partial class temp_unit_email
    {
        [StringLength(50)]
        public string UnitCode { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
    }
}
