using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Project.Booking.Admin.Business.Interfaces;
using Project.Booking.Admin.Configurations;
using Project.Booking.Admin.Constants;
using Project.Booking.Admin.Model;
using System;
using System.Linq;

namespace Project.Booking.Admin.Web.Controllers
{
    public class TransferPaymentController : BaseController
    {
        private readonly ITransferPayment _serviceTFPayment;

        public TransferPaymentController(IMaster serviceMaster,
                              IHttpContextAccessor httpContextAccessor,
                              IOptions<ApplicationSetting> config,
                              ITransferPayment serviceTFPayment) : base(serviceMaster, config, httpContextAccessor)
        {
            _serviceTFPayment = serviceTFPayment;
        }
        public IActionResult Index()
        {
            var cookieProject = Guid.Empty;
            var projects = GetMasterProjectLists(false);
            ViewBag.ProjectSelectList = projects;
            var cookieID = GetCookie(CookieKey.General.ProjectID);
            if (!string.IsNullOrEmpty(cookieID))
                cookieProject = new Guid(GetCookie(CookieKey.General.ProjectID));
            ViewBag.SelectProjectID = cookieProject == Guid.Empty ? projects.FirstOrDefault().Value.ToString() : cookieProject.ToString();
            ViewBag.VerifyStatusList = GetMasterPaymentVerifyStatusLists();
            return View();
        }

        [HttpPost]
        public JsonResult GetPaymentList(DTParamModel param, TransferPayment criteria)
        {
            try
            {
                SetCookie(CookieKey.General.ProjectID, criteria.ProjectID.ToString(), 30);
                var resultData = _serviceTFPayment.GetPaymentList(param, criteria);
                return Json(
                          new
                          {
                              success = true,
                              data = resultData,
                              param.draw,
                              iTotalRecords = param.TotalRowCount,
                              iTotalDisplayRecords = param.TotalRowCount
                          }
                );
            }
            catch (Exception ex)
            {
                return Json(
                            new
                            {
                                success = false,
                                message = InnerException(ex),
                                data = new[] { ex.Message },
                                param.draw,
                                iTotalRecords = 0,
                                iTotalDisplayRecords = 0
                            }
               );
            }
        }
        [HttpPost]
        public JsonResult GetPayment(Guid ID)
        {
            try
            {
                var model = _serviceTFPayment.GetPayment(ID);
                ViewBag.VerifyStatusList = GetMasterPaymentVerifyStatusLists(false)
                                        .Where(e => e.Value != Constant.Ext.PAYMENT_VERIFY_STATUS_VERIFY.ToString()).ToList();
                var html = RenderRazorViewtoString(this, "Partial_Payment_Modal", model);
                return Json(new
                {
                    success = true,
                    html
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
        [HttpPost]
        public JsonResult SavePayment(TransferPayment model)
        {
            try
            {
                validatePayment(model);
                _serviceTFPayment.SavePayment(model);
                return Json(new
                {
                    message = Constant.Message.Success.SAVE_SUCCESS,
                    success = true
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
        private void validatePayment(TransferPayment model)
        {
            if (model.StatusID == Constant.Ext.PAYMENT_VERIFY_STATUS_APPROVE)
            {
                if (model.Quota <= 0) throw new Exception(Constant.Message.Error.TF_PATYMENT_APPROVE_QUOTA_INVALID);
                if (model.ApproveAmount <= 0) throw new Exception(Constant.Message.Error.TF_PATYMENT_APPROVE_AMOUNT_INVALID);
            }
        }
    }
}
