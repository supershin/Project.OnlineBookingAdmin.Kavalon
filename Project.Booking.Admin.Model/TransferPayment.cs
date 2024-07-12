using Project.Booking.Admin.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Booking.Admin.Model
{
    public class TransferPayment : tr_ProjectTransferPayment
    {
        public string SearchStr { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string ProjectName { get; set; }
        public int DiffQuota { get; set; }
        public int TotalQuota { get; set; }
        public int UseQuota { get; set; }
        public string UrlFile { get; set; }
        private ProjectRegisterQuota _RegisterQuota;
        public ProjectRegisterQuota RegisterQuota
        {
            get
            {
                _RegisterQuota = _RegisterQuota ?? new ProjectRegisterQuota();
                return _RegisterQuota;
            }
            set { _RegisterQuota = value; }
        }
    }
}
