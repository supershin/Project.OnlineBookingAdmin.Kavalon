using Microsoft.EntityFrameworkCore;
using Project.Booking.Admin.Business.Interfaces;
using Project.Booking.Admin.Constants;
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
    public class DashboardService : IDashboard
    {
        private readonly OnlineBookingDbContext _context;

        public DashboardService(OnlineBookingDbContext context)
        {
            _context = context;
        }
        public void GetData(DashboardViewModel model)
        {
            model.SummaryUnit = getSummaryUnit(model);
        }
        private SummaryUnit getSummaryUnit(DashboardViewModel criteria)
        {
            var summaryUnit = new SummaryUnit();
            var projectIDs = (!string.IsNullOrEmpty(criteria.ProjectIDs)) ? criteria.ProjectIDs.Split(',').Select(e => Guid.Parse(e)).ToArray() : new Guid[] { };
            var BUIDs = criteria.BUIDs.Split(',').Select(e => int.Parse(e)).ToArray();
            criteria.EndDate = (criteria.EndDate != null) ? criteria.EndDate.AsDate().AddDays(1) : criteria.EndDate;

            var units = _context.tm_Unit
                            .Include(e => e.Project).Where(e => e.FlagActive == true
                              && BUIDs.Contains((int)e.Project.BUID)
                              && (projectIDs.Contains((Guid)e.ProjectID) || projectIDs.Count() == 0)
                              && (e.UserUpdateDate >= criteria.StartDate || criteria.StartDate == null)
                              && (e.UserUpdateDate < criteria.EndDate || criteria.EndDate == null)
                        ).ToList();

            summaryUnit.Total = units.Count();
            summaryUnit.Close = units.Count(e => e.UnitStatusID == Constant.UnitStatus.CLOSE);
            summaryUnit.Available = units.Count(e => e.UnitStatusID == Constant.UnitStatus.AVAILABLE);
            summaryUnit.Booking = units.Count(e => e.UnitStatusID == Constant.UnitStatus.BOOKING);
            summaryUnit.Payment = units.Count(e => e.UnitStatusID == Constant.UnitStatus.PAYMENT);
            return summaryUnit;
        }
    }
}
