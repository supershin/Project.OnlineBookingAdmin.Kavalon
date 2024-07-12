using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Project.Booking.Admin.Data.Models
{
    public partial class temp_pricelist
    {
        [Key]
        [StringLength(50)]
        public string UnitCode { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? SellingPrice { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? BookingAmount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? ContractAmount { get; set; }
    }
}
