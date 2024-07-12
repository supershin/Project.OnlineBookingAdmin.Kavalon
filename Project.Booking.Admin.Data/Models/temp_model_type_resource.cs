using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Project.Booking.Admin.Data.Models
{
    [Keyless]
    public partial class temp_model_type_resource
    {
        public int? ModelTypeID { get; set; }
        [StringLength(255)]
        public string ModelTypeName { get; set; }
        [StringLength(255)]
        public string ResourceID { get; set; }
        [StringLength(255)]
        public string FileName { get; set; }
    }
}
