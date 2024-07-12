using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Booking.Admin.Model
{
    public class RegisterViewModel
    {
        public Guid ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string CitizenID { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public DateTime? LastSignInDate { get; set; }
        public DateTime? LastSignOutDate { get; set; }
        public bool IsOnline
        {
            get
            {
                bool isOnline = false;
                if (this.LastSignInDate > this.LastSignOutDate)
                    isOnline = true;
                else if (this.LastSignInDate != null && this.LastSignOutDate == null)
                    isOnline = true;                
                return isOnline;
            }
        }
        public string RegisterTypeName { get; set; }
        public int? RegisterTypeID { get; set; }
        public bool FlagActive { get; set; }
        private List<ProjectRegisterQuota> _ProjectQuotas;
        public List<ProjectRegisterQuota> ProjectQuotas
        {
            get {
                _ProjectQuotas = _ProjectQuotas ?? new List<ProjectRegisterQuota>();
                return _ProjectQuotas; }
            set { _ProjectQuotas = value; }
        }

    }
}
