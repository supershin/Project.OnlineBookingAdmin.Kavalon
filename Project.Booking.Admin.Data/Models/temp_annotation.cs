using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Project.Booking.Admin.Data.Models
{
    [Keyless]
    public partial class temp_annotation
    {
        [StringLength(255)]
        public string Build { get; set; }
        public int? Room { get; set; }
        [StringLength(255)]
        public string AnnotationID { get; set; }
        public string Annotation { get; set; }
        [StringLength(100)]
        public string SelectorType { get; set; }
    }
}
