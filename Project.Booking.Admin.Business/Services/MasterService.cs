using Project.Booking.Admin.Business.Interfaces;
using Project.Booking.Admin.Data.Models;
using Project.Booking.Admin.Extensions;
using Project.Booking.Admin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Booking.Admin.Business.Services
{
    public class MasterService : IMaster
    {
        private readonly OnlineBookingDbContext _context;
        public MasterService(OnlineBookingDbContext context)
        {
            _context = context;
        }
        public List<tm_Ext> GetMasterExt(int ExtTypeID)
        {
            return _context.tm_Ext.Where(e => e.FlagActive == true && e.ExtTypeID == ExtTypeID)
                    .OrderBy(e => e.LineOrder).ToList();
        }
        public List<tm_Department> GetMasterDepartment()
        {
            return _context.tm_Department.Where(e => e.FlagActive == true )
                    .OrderBy(e => e.LineOrder).ToList();
        }
        public List<tm_Role> GetMasterRole()
        {
            return _context.tm_Role.Where(e => e.FlagActive == true )
                    .OrderBy(e => e.LineOrder).ToList();
        }        
        public List<tm_UnitStatus> GetMasterUnitStatus()
        {
            return _context.tm_UnitStatus.Where(e => e.FlagActive == true)
                    .OrderBy(e => e.LineOrder).ToList();
        }
        public List<tm_BookingStatus> GetMasterBookingStatus()
        {
            return _context.tm_BookingStatus.Where(e => e.FlagActive == true)
                    .OrderBy(e => e.LineOrder).ToList();
        }
        public List<ProjectModel> GetMasterProject(int departmentID)
        {
            var projectIDs = getProjectPermission(departmentID).Select(e => e.ProjectID).ToArray();
            return _context.tm_Project.Where(e => e.FlagActive == true
                    && (projectIDs.Contains(e.ID) || projectIDs.Count() == 0))
                .Select(e => new ProjectModel
                {
                    BUID = e.BUID.AsInt(),
                    ProjectID = e.ID,
                    ProjectCode = e.ProjectCode,
                    ProjectName = e.ProjectNameTH
                }).OrderBy(e => e.ProjectCode).ToList();
        }
        private List<ProjectModel> getProjectPermission(int departmentID)
        {
            var query = from db in _context.tr_DepartmentBU_Mapping.Where(e => e.DepartmentID == departmentID)
                        join bu in _context.tm_BU.Where(e => e.FlagActive == true)
                          on db.BUID equals bu.ID
                        join p in _context.tm_Project.Where(e => e.FlagActive == true)
                          on bu.ID equals p.BUID
                        orderby p.ProjectNameTH
                        select new { bu, p };
            if (query.Any())
            {
                var projects = query.AsEnumerable().Select(e => new ProjectModel
                {
                    BUID = e.bu.ID,
                    BUName = e.bu.Name,
                    ProjectID = e.p.ID,
                    ProjectCode = e.p.ProjectCode
                }).ToList();
                return projects;
            }
            return new List<ProjectModel>();
        }
        public List<BUModel> GetMasterBU(int departmentID)
        {
            var buIDs = _context.tr_DepartmentBU_Mapping.Where(e => e.DepartmentID == departmentID)
                        .Select(e => e.BUID).ToArray();
            return _context.tm_BU.Where(e => buIDs.Contains(e.ID) || buIDs.Count() == 0)
                .Select(e => new BUModel
                {
                    ID = e.ID,
                    Name = e.Name
                }).OrderBy(e => e.Name).ToList();
        }
        public List<ProjectModel> GetMasterProjectByBU(string buIDs)
        {

            var arrBUID = (!string.IsNullOrEmpty(buIDs)) ? buIDs.Split(',').Select(e => int.Parse(e)).ToArray() :
                            new int[] { };
            return _context.tm_Project.Where(e => e.FlagActive == true
                    && (arrBUID.Contains((int)e.BUID)))
                    .Select(e => new ProjectModel
                    {
                        BUID = e.BUID.AsInt(),
                        ProjectID = e.ID,
                        ProjectCode = e.ProjectCode,
                        ProjectName = e.ProjectNameTH
                    }).OrderBy(e => e.ProjectName).ToList();
        }
    }
}
