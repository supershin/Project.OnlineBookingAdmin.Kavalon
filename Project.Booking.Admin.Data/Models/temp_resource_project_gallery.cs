using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Project.Booking.Admin.Data.Models
{
    [Keyless]
    public partial class temp_resource_project_gallery
    {
        [StringLength(255)]
        public string ResourceID { get; set; }
        [StringLength(255)]
        public string FileName { get; set; }
        [StringLength(255)]
        public string FilePath { get; set; }
        [StringLength(255)]
        public string MimeType { get; set; }
        public double? ProjectGalleryID { get; set; }
        public double? LineOrder { get; set; }
    }
}
