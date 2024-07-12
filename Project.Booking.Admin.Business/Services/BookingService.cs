using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Project.Booking.Admin.Business.Extensions;
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
using System.Transactions;

namespace Project.Booking.Admin.Business.Services
{
    public class BookingService : IBooking
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnit _serviceUnit;
        private readonly OnlineBookingDbContext _context;
        private readonly UserProfile _userProfile;
        public BookingService(OnlineBookingDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IUnit serviceUnit)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _serviceUnit = serviceUnit;
            _userProfile = _httpContextAccessor.HttpContext.User.GetUserProfile();
        }
        public IEnumerable<BookingViewModel> GetBookingByProjectAll(Guid projectID)
        {
            var query = from b in _context.ts_Booking.Where(e => e.FlagActive == true && e.ProjectID == projectID)
                                  .Include(e => e.Unit)
                                  .Include(e => e.BookingStatus)
                                  .Include(e => e.UserUpdateBy)
                        select new { b };
            return query.AsEnumerable().Select(e => new BookingViewModel
            {
                BookingID = e.b.ID,
                BookingNumber = e.b.BookingNumber,
                UnitCode = e.b.Unit.UnitCode,
                BookingStatusID = e.b.BookingStatusID,
                BookingStatusName = e.b.BookingStatus.Name,
                BookingStatusColor = e.b.BookingStatus.Color,
                BookingDate = e.b.BookingDate,
                PaymentDueDate = e.b.PaymentDueDate,
                OverDue = e.b.PaymentOverDueDate,
                CustomerName = $"{e.b.CustomerFirstName} {e.b.CustomerLastName}",
                CustomerMobile = e.b.CustomerMobile,
                BookingAmount = e.b.BookingAmount,
                SellingPrice = e.b.SellingPrice,
                Discount = e.b.Discount,
                SpecialPrice = e.b.SpecialPrice,
                CancelDate = e.b.CancelDate,
                UserUpdateDate = e.b.UserUpdateDate,
                UserUpdateByName = (e.b.UserUpdateBy != null) ? e.b.UserUpdateBy.FirstName : null
            }).OrderByDescending(e => e.UserUpdateDate).ToList();
        }
        public BookingViewModel GetBookingByID(Guid bookingID)
        {
            var query = from b in _context.ts_Booking.Where(e => e.FlagActive == true && e.ID == bookingID)
                                  .Include(e => e.Project)
                                  .Include(e => e.Unit).Include(e => e.Unit.UnitType)
                                  .Include(e => e.BookingStatus)
                                  .Include(e => e.UserUpdateBy)
                                  .Include(e => e.ts_Payment).ThenInclude(e => e.PaymentType)
                                  .Include(e => e.ts_Payment).ThenInclude(e=>e.ts_Payment_Credit)
                        select new { b };
            return query.AsEnumerable().Select(e => new BookingViewModel
            {
                ProjectName = e.b.Project.ProjectNameTH,
                BookingID = e.b.ID,
                BookingNumber = e.b.BookingNumber,
                UnitID = e.b.UnitID,
                UnitCode = e.b.Unit.UnitCode,
                UnitTypeName = e.b.Unit.UnitType.Name,
                BookingStatusID = e.b.BookingStatusID,
                BookingStatusName = e.b.BookingStatus.Name,
                BookingStatusColor = e.b.BookingStatus.Color,
                BookingDate = e.b.BookingDate,
                CustomerName = $"{e.b.CustomerFirstName} {e.b.CustomerLastName}",
                CutizenID = e.b.CustomerCitizenID,
                CustomerEmail = e.b.CustomerEmail,
                CustomerMobile = e.b.CustomerMobile,
                BookingAmount = e.b.BookingAmount,
                SellingPrice = e.b.SellingPrice,
                Discount = e.b.Discount,
                SpecialPrice = e.b.SpecialPrice,
                PaymentDueDate = e.b.PaymentDueDate,
                CancelDate = e.b.CancelDate,
                UserUpdateDate = e.b.UserUpdateDate,
                UserUpdateByName = (e.b.UserUpdateBy != null) ? e.b.UserUpdateBy.FirstName : null,
                Payments = e.b.ts_Payment.AsEnumerable().Select(e => new PaymentViewModel
                {
                    PaymentNo = e.PaymentNo,
                    PaymentDate = e.PaymentDate,
                    PaymentTypeName = e.PaymentType.Name,
                    PaymentCredit = e.ts_Payment_Credit.AsEnumerable().Select(e => new PaymentCreditViewModel
                    {
                        CardName = e.CardName,
                        CardBank = e.CardBank,
                        CardLastDigit = e.CardLastDigit,
                        CardBrand = e.CardBrand,
                        Amount = e.Amount,
                        Status = e.Status,
                        FailureMessage = e.FailureMessage
                    }).SingleOrDefault()
                }).OrderByDescending(e => e.PaymentDate)
            }).OrderByDescending(e => e.UserUpdateDate).SingleOrDefault();
        }
        public void SaveCancelBooking(BookingViewModel model)
        {
            using (var scope = new TransactionScope())
            {
                SaveCancelBookingData(model);                
                _serviceUnit.UpdateUnitStatus(model.UnitID.AsGuid(), Constant.UnitStatus.AVAILABLE);
                _serviceUnit.SaveUnitBookingHistory(model.UnitID.AsGuid(),
                        Constant.UnitStatus.AVAILABLE,
                        model.BookingID,
                        model.BookingStatusID);
                scope.Complete();
            }
        }
        private void SaveCancelBookingData(BookingViewModel model)
        {
            var item = _context.ts_Booking.Where(e => e.ID == model.BookingID && e.FlagActive == true).SingleOrDefault();
            if (item.BookingStatusID == Constant.BookingStatus.CANCEL)
            {
                throw new Exception(Constant.Message.Error.INVALID_BOOKING_HAS_CANCEL);
            }
            if (item != null)
            {
                item = SetBookingData(item, model);
                _context.Update(item);
                _context.SaveChanges();
            }

        }
        private ts_Booking SetBookingData(ts_Booking item, BookingViewModel model)
        {
            model.BookingStatusID = Constant.BookingStatus.CANCEL;
            item.BookingStatusID = model.BookingStatusID;
            item.CancelDate = DateTime.Now;
            item.UserUpdateDate = DateTime.Now;
            item.UserUpdateByID = _userProfile.ID;
            return item;
        }        
    }
}
