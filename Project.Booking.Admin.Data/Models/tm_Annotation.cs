using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Project.Booking.Admin.Data.Models
{
    public partial class tm_Annotation
    {
        public tm_Annotation()
        {
            tm_Unit = new HashSet<tm_Unit>();
        }

        [Key]
        public Guid ID { get; set; }
        [StringLength(20)]
        public string SelectorType { get; set; }
        public string SelectorValue { get; set; }

        [InverseProperty("Annotation")]
        public virtual ICollection<tm_Unit> tm_Unit { get; set; }
    }
}
