using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Options;
using Project.Booking.Admin.Business.Extensions;
using Project.Booking.Admin.Business.Interfaces;
using Project.Booking.Admin.Configurations;
using Project.Booking.Admin.Constants;
using Project.Booking.Admin.Data.Models;
using Project.Booking.Admin.Extensions;
using Project.Booking.Admin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Project.Booking.Admin.Business.Services
{
    public class TransferPaymentService : ITransferPayment
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRegister _serviceRegister;
        private readonly IOptions<ApplicationSetting> _config;
        private readonly OnlineBookingDbContext _context;
        private readonly UserProfile _userProfile;
        public TransferPaymentService(OnlineBookingDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IRegister serviceRegister,
            IOptions<ApplicationSetting> config)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _serviceRegister = serviceRegister;
            _config = config;
            _userProfile = _httpContextAccessor.HttpContext.User.GetUserProfile();
        }

        public dynamic GetPaymentList(DTParamModel param, TransferPayment criteria)
        {
            var totalRecord = 0;
            bool asc = param.sortDirection.ToUpper().Contains("ASC");
            criteria.SearchStr = criteria.SearchStr.ToStringNullable() ?? string.Empty;

            var query = from tp in _context.tr_ProjectTransferPayment.Where(e => e.ProjectID == criteria.ProjectID
                                    && e.FlagActive == true)
                        join r in _context.tm_Register
                           on tp.RegisterID equals r.ID
                        join st in _context.tm_Ext
                          on tp.StatusID equals st.ID
                        join vf in _context.tm_User
                            on tp.VerifyByID equals vf.ID into _vf
                        from vf2 in _vf.DefaultIfEmpty()
                        join f in _context.tm_Resource
                            on tp.ResourceID equals f.ID into _f
                        from f2 in _f.DefaultIfEmpty()
                        where (tp.StatusID == criteria.StatusID || criteria.StatusID == null)
                        && tp.ProjectID == criteria.ProjectID
                        && (r.FirstName.Contains(criteria.SearchStr)
                         || r.LastName.Contains(criteria.SearchStr)
                         || r.Email.Contains(criteria.SearchStr) || criteria.SearchStr == string.Empty)
                        select new
                        {
                            tp.ID,
                            tp.RegisterID,
                            CustomerName = r.FirstName + " " + r.LastName,
                            r.Email,
                            tp.TransferDate,
                            tp.Amount,
                            tp.ApproveAmount,
                            tp.Quota,
                            tp.StatusID,
                            StatusName = st.Name,
                            tp.VerifyDate,
                            VerifyBy = (vf2 != null) ? vf2.FirstName : null,
                            tp.UpdateDate,
                            FilePath = (f2 != null) ? f2.FilePath : null
                        };
            var result = query.Page(param.start, param.length, i => i.UpdateDate, param.SortColumnName, asc, out totalRecord);
            param.TotalRowCount = totalRecord;
            return result.AsEnumerable().Select(e => new
            {
                e.ID,
                e.RegisterID,
                e.CustomerName,
                e.Email,
                TransferDate = e.TransferDate.ToStringDate(),
                Amount = e.Amount.ToStringNumber(),
                ApproveAmount = e.ApproveAmount.ToStringNumber(),
                e.Quota,
                e.StatusID,
                e.StatusName,
                VerifyDate = e.VerifyDate.ToStringDateTime(),
                VerifyBy = e.VerifyBy.ToStringEmpty(),
                UpdateDate = e.UpdateDate.ToStringDateTime(),
                UrlFile = $"{_config.Value.UrlSalekit}{e.FilePath}"
            }).ToList();
        }
        public TransferPayment GetPayment(Guid ID)
        {
            var query = from tp in _context.tr_ProjectTransferPayment.Where(e => e.ID == ID)
                        join f in _context.tm_Resource
                           on tp.ResourceID equals f.ID
                        join p in _context.tm_Project
                            on tp.ProjectID equals p.ID
                        select new { tp, f, p };

            var data = query.Select(e => new TransferPayment
            {
                ID = e.tp.ID,
                ProjectID = e.tp.ProjectID,
                ProjectName = e.p.ProjectNameTH,
                TransferDate = e.tp.TransferDate,
                Amount = e.tp.Amount,
                ApproveAmount = e.tp.ApproveAmount,
                Quota = e.tp.Quota,
                RegisterID = e.tp.RegisterID,
                StatusID = e.tp.StatusID,
                UrlFile = $"{_config.Value.UrlSalekit}{e.f.FilePath}",
                FileName = e.f.FileName
            }).FirstOrDefault();

            var quota = _context.tr_ProjectRegisterQuota.FirstOrDefault(e => e.ProjectID == data.ProjectID && e.RegisterID == data.RegisterID
                        && e.FlagActive == true);
            if (quota != null)
            {
                var registerQuota = _serviceRegister.GetProjectQuotaByID(quota.ID);
                data.TotalQuota = registerQuota.Quota.AsInt();
                data.UseQuota = registerQuota.UseQuota.AsInt();
            }
            return data;
        }
        public void SavePayment(TransferPayment model)
        {
            using (var scope = new TransactionScope())
            {
                savePaymentData(model);
                saveRegisterQuota(model);
                scope.Complete();
            }
        }
        private void savePaymentData(TransferPayment model)
        {
            var item = _context.tr_ProjectTransferPayment.FirstOrDefault(e => e.ID == model.ID);
            if (item.VerifyByID != null && item.VerifyByID != _userProfile.ID)
                throw new Exception(Constant.Message.Error.TF_PATYMENT_VERIFY_ANOTHER_USER);
            item = setPayment(item, model);
            _context.Update(item);
            _context.SaveChanges();
        }
        private tr_ProjectTransferPayment setPayment(tr_ProjectTransferPayment item, TransferPayment model)
        {
            if (model.StatusID == Constant.Ext.PAYMENT_VERIFY_STATUS_APPROVE)
                model.DiffQuota = model.Quota.AsInt() - item.Quota.AsInt();
            else if (model.StatusID == Constant.Ext.PAYMENT_VERIFY_STATUS_FAIL)
                model.DiffQuota = model.Quota.AsInt() * -1;

            item.ApproveAmount = model.ApproveAmount;
            item.StatusID = model.StatusID;
            item.Quota = model.Quota;
            item.VerifyDate = DateTime.Now;
            item.VerifyByID = _userProfile.ID;
            item.UpdateDate = DateTime.Now;
            item.UpdateBy = _userProfile.ID;
            return item;
        }
        private void saveRegisterQuota(TransferPayment model)
        {            

            var item = _context.tr_ProjectRegisterQuota.FirstOrDefault(e => e.ProjectID == model.ProjectID
                        && e.RegisterID == model.RegisterID)?? new tr_ProjectRegisterQuota();
           
            var register = new ProjectRegisterQuota();
            register.ID = item.ID;
            register.ProjectID = model.ProjectID;
            register.RegisterID = model.RegisterID;
            register.Quota = item.Quota.AsInt() + model.DiffQuota;

            _serviceRegister.SaveProjectQuotaData(register);
        }
        
    }
}
