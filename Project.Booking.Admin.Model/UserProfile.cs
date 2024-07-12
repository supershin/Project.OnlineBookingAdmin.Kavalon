using Project.Booking.Admin.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Booking.Admin.Model
{
    public class UserProfile 
    {
        public Guid ID { get; set; }
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        public string Email { get; set; }        
        public string Mobile { get; set; }
        public int? DepartmentID { get; set; }
        public int? RoleID { get; set; }
        public string DepartmentName { get; set; }
        public string RoleName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateByName { get; set; }

        public bool isUserProfile { get; set; }
        public bool RememberLogin { get; set; }
        public int RoleLevel { get; set; }
    }
}
