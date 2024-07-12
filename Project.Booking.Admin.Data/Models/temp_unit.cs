using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Project.Booking.Admin.Data.Models
{
    [Keyless]
    public partial class temp_unit
    {
        [StringLength(255)]
        public string Build { get; set; }
        [StringLength(255)]
        public string UnitCode { get; set; }
        public double? Floor { get; set; }
        public double? Room { get; set; }
        [StringLength(255)]
        public string ModelType { get; set; }
        public double? Area { get; set; }
        [StringLength(255)]
        public string UnitType { get; set; }
        public double? SellingPrice { get; set; }
        public double? BookingAmount { get; set; }
        public double? ContractAmount { get; set; }
    }
}
