using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Project.Booking.Admin.Data.Models
{
    public partial class tm_Register
    {
        public tm_Register()
        {
            tr_ProjectRegisterQuota = new HashSet<tr_ProjectRegisterQuota>();
            tr_ProjectTransferPayment = new HashSet<tr_ProjectTransferPayment>();
            ts_Booking = new HashSet<ts_Booking>();
        }

        [Key]
        public Guid ID { get; set; }
        public int? RegisterTypeID { get; set; }
        [StringLength(1000)]
        public string Code { get; set; }
        [StringLength(1000)]
        public string FirstName { get; set; }
        [StringLength(1000)]
        public string LastName { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        [StringLength(100)]
        public string Mobile { get; set; }
        [StringLength(50)]
        public string CitizenID { get; set; }
        [StringLength(200)]
        public string Password { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? AllowBookDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ActivateDate { get; set; }
        public bool? FlagActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastSignInDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastSignOutDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateDate { get; set; }
        public Guid? CreateBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdateDate { get; set; }
        public Guid? UpdateBy { get; set; }

        [ForeignKey(nameof(RegisterTypeID))]
        [InverseProperty(nameof(tm_Ext.tm_Register))]
        public virtual tm_Ext RegisterType { get; set; }
        [InverseProperty("Register")]
        public virtual ICollection<tr_ProjectRegisterQuota> tr_ProjectRegisterQuota { get; set; }
        [InverseProperty("Register")]
        public virtual ICollection<tr_ProjectTransferPayment> tr_ProjectTransferPayment { get; set; }
        [InverseProperty("Customer")]
        public virtual ICollection<ts_Booking> ts_Booking { get; set; }
    }
}
