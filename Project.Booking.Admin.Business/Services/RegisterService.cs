
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Project.Booking.Admin.Business.Interfaces;
using Project.Booking.Admin.Constants;
using Project.Booking.Admin.Extensions;
using Project.Booking.Admin.Data.Models;
using Project.Booking.Admin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Project.Booking.Admin.Business.Extensions;

namespace Project.Booking.Admin.Business.Services
{
    public class RegisterService : IRegister
    {
        private readonly OnlineBookingDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private UserProfile _userProfile;
        public RegisterService(OnlineBookingDbContext context,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _userProfile = _httpContextAccessor.HttpContext.User.GetUserProfile();
        }
        public IEnumerable<RegisterViewModel> GetRegisterAll()
        {
            return _context.tm_Register.Include(t => t.RegisterType)
                    .AsEnumerable().Select(e => new RegisterViewModel
                    {
                        ID = e.ID,
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        Email = e.Email,
                        Mobile = e.Mobile,
                        CitizenID = e.CitizenID,
                        LastUpdateDate = e.UpdateDate,
                        LastSignInDate = e.LastSignInDate,
                        LastSignOutDate = e.LastSignOutDate,
                        RegisterTypeName = e.RegisterType.Name,
                        FlagActive = e.FlagActive.AsBool()
                    }).OrderByDescending(e => e.LastUpdateDate).ToList();
        }
        public RegisterViewModel GetRegisterByID(Guid id)
        {
            var item = _context.tm_Register.Where(e =>e.ID == id)
                            .Select(e => new RegisterViewModel
                            {
                                ID = e.ID,
                                FirstName = e.FirstName,
                                LastName = e.LastName,
                                Email = e.Email,
                                Mobile = e.Mobile,
                                Password = e.Password,
                                ConfirmPassword = e.Password,
                                RegisterTypeID = e.RegisterTypeID,
                                FlagActive = e.FlagActive.AsBool()
                            }).FirstOrDefault() ?? new RegisterViewModel { FlagActive = true};

            item.ProjectQuotas = getProjectRegisterQuotaList(item.ID);
            return item;
        }
        public void SaveRegister(RegisterViewModel model)
        {
            using (var scope = new TransactionScope())
            {
                saveRegisterData(model);
                scope.Complete();
            }
        }
        private void saveRegisterData(RegisterViewModel model)
        {
            verifyEmail(model.ID, model.Email.ToStringNullable());
            var item = _context.tm_Register.SingleOrDefault(e => e.ID == model.ID);
            if (item == null)
            {
                item = setRegisterData(new tm_Register(), model);
                _context.Add(item);
            }
            else
            {
                item = setRegisterData(item, model);
                _context.Update(item);
            }
            _context.SaveChanges();
        }
        private void verifyEmail(Guid userID, string email)
        {
            if (_context.tm_Register.Any(e => e.Email == email && e.ID != userID))
            {
                throw new Exception(Constant.Message.Error.EMIAL_HAS_ALREADY);
            }
        }
        private tm_Register setRegisterData(tm_Register item, RegisterViewModel model)
        {
            if (item.ID == Guid.Empty)
            {
                item.ID = Guid.NewGuid();
                model.ID = item.ID;                
                item.CreateDate = DateTime.Now;
                item.CreateBy = _userProfile.ID;                
            }
            item.FirstName = model.FirstName.ToStringNullable();
            item.RegisterTypeID = model.RegisterTypeID;
            item.Email = model.Email.ToStringNullable();
            item.Mobile = model.Mobile.ToStringNullable();
            item.Password = model.Password.ToStringNullable();
            item.FlagActive = model.FlagActive;
            item.UpdateDate = DateTime.Now;
            item.UpdateBy = _userProfile.ID;
            return item;
        }
        private List<ProjectRegisterQuota> getProjectRegisterQuotaList(Guid registerID)
        {
            return _context.tr_ProjectRegisterQuota.Where(e => e.FlagActive == true && e.RegisterID == registerID)
                    .Include(p => p.Project)
                    .Select(e => new ProjectRegisterQuota
                    {
                        ID = e.ID,
                        ProjectID = e.ProjectID,
                        ProjectName = e.Project.ProjectNameEN,
                        Quota = e.Quota,
                        UseQuota = _context.ts_Booking.Where(b => b.ProjectID == e.ProjectID
                            && b.CustomerID == registerID && b.FlagActive == true
                            && b.BookingStatusID != Constant.BookingStatus.CANCEL
                            && b.BookingStatusID != null && b.BookingStatusID != 0).Count(),
                        UpdateDate = e.UpdateDate
                    }).OrderByDescending(e=>e.UpdateDate).ToList();

        }
        public ProjectRegisterQuota GetProjectQuotaByID(int ID)
        {
            return _context.tr_ProjectRegisterQuota.Where(e => e.FlagActive == true && e.ID == ID)
                    .Include(p => p.Project)
                    .Select(e => new ProjectRegisterQuota
                    {
                        ID = e.ID,
                        ProjectID = e.ProjectID,
                        ProjectName = e.Project.ProjectNameEN,
                        Quota = e.Quota,
                        UseQuota = _context.ts_Booking.Where(b => b.ProjectID == e.ProjectID
                            && b.CustomerID == e.RegisterID && b.FlagActive == true
                            && b.BookingStatusID != Constant.BookingStatus.CANCEL
                            && b.BookingStatusID != null && b.BookingStatusID != 0).Count()
                    }).FirstOrDefault() ?? new ProjectRegisterQuota();
        }
        public void SaveProjectQuota(ProjectRegisterQuota model)
        {
            using (var scope = new TransactionScope())
            {
                SaveProjectQuotaData(model);
                scope.Complete();
            }
        }
        public void SaveProjectQuotaData(ProjectRegisterQuota model) {
            validateProjectQuota(model);
            var item = _context.tr_ProjectRegisterQuota.Where(e => e.RegisterID == model.RegisterID
                                && e.ProjectID == model.ProjectID && e.FlagActive == true).FirstOrDefault();
            if (item == null) {
                item = setProjectQuota(new tr_ProjectRegisterQuota(),model); ;
                _context.Add(item);
            }
            else {
                item = setProjectQuota(item, model); ;
                _context.Update(item);
            }
            _context.SaveChanges();
        }
        private void validateProjectQuota(ProjectRegisterQuota model) {
            var totalBooked = _context.ts_Booking.Where(b => b.ProjectID == model.ProjectID
                                && b.CustomerID == model.RegisterID && b.FlagActive == true
                                && b.BookingStatusID != Constant.BookingStatus.CANCEL
                                && b.BookingStatusID != null && b.BookingStatusID != 0).Count();
            if (totalBooked > model.Quota)
                throw new Exception(Constant.Message.Error.PROJECT_QUOTA_INVALID);
        }
        private tr_ProjectRegisterQuota setProjectQuota(tr_ProjectRegisterQuota item, ProjectRegisterQuota model) {
            if (item.ID == 0)
            {
                item.ProjectID = model.ProjectID;
                item.RegisterID = model.RegisterID;
                item.FlagActive = true;
                item.CreateDate = DateTime.Now;
                item.CreateBy = _userProfile.ID;
            }
            item.Quota = model.Quota.AsInt();
            item.UpdateDate = DateTime.Now;
            item.UpdateBy = _userProfile.ID;
            return item;
        }
    }
}
