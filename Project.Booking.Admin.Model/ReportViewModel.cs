using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Booking.Admin.Model
{
    public class ReportViewModel
    {
        public string BUIDs { get; set; }
        public Guid ProjectID { get; set; }
        public string ProjectIDs { get; set; }
        public string UnitStatusIDs { get; set; }
        public string OrderStatusIDs { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        //private List<BUModel> _BUModel;
        //public List<BUModel> BUList
        //{
        //    get { return _BUModel ?? new List<BUModel>(); }
        //    set { _BUModel = value; }
        //}
        //private List<ProjectModel> _ProjectModel;
        //public List<ProjectModel> ProjectList
        //{
        //    get { return _ProjectModel ?? new List<ProjectModel>(); }
        //    set { _ProjectModel = value; }
        //}
    }
}
