using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Project.Booking.Admin.Data.Models
{
    [Index(nameof(Username), Name = "NonClusteredIndex-20210909-142758", IsUnique = true)]
    [Index(nameof(Email), Name = "NonClusteredIndex-20210909-142816", IsUnique = true)]
    public partial class tm_User
    {
        public tm_User()
        {
            InverseUpdateByNavigation = new HashSet<tm_User>();
            tm_Unit = new HashSet<tm_Unit>();
            ts_Booking = new HashSet<ts_Booking>();
        }

        [Key]
        public Guid ID { get; set; }
        [StringLength(200)]
        public string FirstName { get; set; }
        [StringLength(500)]
        public string LastName { get; set; }
        public int? DepartmentID { get; set; }
        public int? RoleID { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        [StringLength(100)]
        public string Mobile { get; set; }
        [StringLength(100)]
        public string Username { get; set; }
        public string Password { get; set; }
        public bool? FlagActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateDate { get; set; }
        public Guid? CreateBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdateDate { get; set; }
        public Guid? UpdateBy { get; set; }

        [ForeignKey(nameof(DepartmentID))]
        [InverseProperty(nameof(tm_Department.tm_User))]
        public virtual tm_Department Department { get; set; }
        [ForeignKey(nameof(RoleID))]
        [InverseProperty(nameof(tm_Role.tm_User))]
        public virtual tm_Role Role { get; set; }
        [ForeignKey(nameof(UpdateBy))]
        [InverseProperty(nameof(tm_User.InverseUpdateByNavigation))]
        public virtual tm_User UpdateByNavigation { get; set; }
        [InverseProperty(nameof(tm_User.UpdateByNavigation))]
        public virtual ICollection<tm_User> InverseUpdateByNavigation { get; set; }
        [InverseProperty("UserUpdateBy")]
        public virtual ICollection<tm_Unit> tm_Unit { get; set; }
        [InverseProperty("UserUpdateBy")]
        public virtual ICollection<ts_Booking> ts_Booking { get; set; }
    }
}
