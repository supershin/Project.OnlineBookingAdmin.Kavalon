using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Project.Booking.Admin.Data.Models
{
    [Keyless]
    public partial class temp_quota_31
    {
        public int ID { get; set; }
        public Guid? ProjectID { get; set; }
        public Guid? RegisterID { get; set; }
        [StringLength(255)]
        public string Email { get; set; }
        public double? Quota { get; set; }
    }
}
