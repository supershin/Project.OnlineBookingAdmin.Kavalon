using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Project.Booking.Admin.Data.Models
{
    public partial class temp_allow_book
    {
        [Key]
        public int ID { get; set; }
        [StringLength(500)]
        public string Name { get; set; }
        [StringLength(200)]
        public string Email { get; set; }
        public int? Quota { get; set; }
        [StringLength(500)]
        public string Agency { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? AllowBookDate { get; set; }
    }
}
