using Microsoft.AspNetCore.Mvc;
using Project.Booking.Admin.Business.Interfaces;
using Project.Booking.Admin.Extensions;
using Project.Booking.Admin.Constants;
using Project.Booking.Admin.Model;
using Project.Booking.Admin.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Project.Booking.Admin.Configurations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Project.Booking.Admin.Web.Controllers
{
    //[CustomAuthenticationFilter]
    [Authorize]
    public class BookingController : BaseController
    {
        private readonly IUnit _serviceUnit;
        private readonly IBooking _serviceBooking;
        public BookingController(
            IMaster serviceMaster,
            IBooking serviceBooking,
             IUnit serviceUnit,
             IHttpContextAccessor httpContextAccessor,
            IOptions<ApplicationSetting> config) : base(serviceMaster, config, httpContextAccessor)
        {
            _serviceBooking = serviceBooking;
            _serviceUnit = serviceUnit;
        }
        public IActionResult Index()
        {
            var projects = GetMasterProjectLists(false);
            ViewBag.ProjectSelectList = projects;
            ViewBag.SelectProjectID = GetCookie(CookieKey.General.ProjectID) ?? projects.FirstOrDefault().Value;
            return View();
        }
        [HttpPost]
        public JsonResult GetBookingByProjectAll(Guid ProjectID)
        {
            try
            {
                SetCookie(CookieKey.General.ProjectID, ProjectID.ToString(), 30);
                var model = _serviceBooking.GetBookingByProjectAll(ProjectID);
                var html = RenderRazorViewtoString(this, "Partial_Booking_List", model);
                return Json(new
                {
                    success = true,
                    html = html
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = InnerException(ex)
                });
            }
        }

        public IActionResult Manage(Guid? ID)
        {
            var model = _serviceBooking.GetBookingByID(ID.AsGuid());
            ViewBag.BookingStatusSelectList = GetMasterBookingStatusLists()
                                                        .Where(e => e.Value == Constant.BookingStatus.CANCEL.ToString()
                                                        || e.Value == Constant.BookingStatus.WAIT_PAYMENT.ToString());
            return View(model);
        }
        [HttpPost]
        public IActionResult SaveCancelBooking(BookingViewModel model)
        {
            try
            {
                _serviceBooking.SaveCancelBooking(model);
                model = _serviceBooking.GetBookingByID(model.BookingID);
                var unit = _serviceUnit.GetUnitByID(model.UnitID.AsGuid());
                return Json(new { 
                    success = true,
                    message = Constant.Message.Success.SAVE_SUCCESS,
                    unit = unit,
                    html = RenderRazorViewtoString(this, "Partial_Booking_Form", model) });
            }
            catch (Exception ex)
            {
                model = _serviceBooking.GetBookingByID(model.BookingID);
                ViewBag.ErrorMsg = InnerException(ex);               
                return JsonPartialViewError("Partial_Booking_Form", model, InnerException(ex));
            }
        }
    }
}
