using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using Project.Booking.Admin.Business.Interfaces;
using Project.Booking.Admin.Configurations;
using Project.Booking.Admin.Constants;
using Project.Booking.Admin.Extensions;
using Project.Booking.Admin.Model;
using Project.Booking.Admin.Web.Filters;

namespace Project.Booking.Admin.Web.Controllers
{
    //[CustomAuthenticationFilter]
    [Authorize]
    public class RegisterController : BaseController
    {
        private readonly IMaster _serviceMaster;
        private readonly IRegister _serviceRegister;
        public RegisterController(
            IMaster serviceMaster,
            IRegister serviceRegister,
            IHttpContextAccessor httpContextAccessor,
            IOptions<ApplicationSetting> config) : base(serviceMaster, config, httpContextAccessor)
        {
            _serviceMaster = serviceMaster;
            _serviceRegister = serviceRegister;
        }
        public IActionResult Index()
        {
            var model = _serviceRegister.GetRegisterAll();
            return View(model);
        }

        public IActionResult Manage(Guid? ID = null)
        {
            ViewBag.RegisterTypeSelectList = GetMasterExtLists(Constant.ExtType.REGISTER_TYPE_ID);
            var projects = _serviceMaster.GetMasterProject(userProfile.DepartmentID.AsInt()).Select(e=>e.ProjectID).ToArray();            
            var model = _serviceRegister.GetRegisterByID(ID.AsGuid());
            model.ProjectQuotas = model.ProjectQuotas.Where(e => projects.Contains((Guid)e.ProjectID)).ToList();
            return View(model);
        }
        [HttpPost]
        public JsonResult SaveRegister(RegisterViewModel model)
        {
            try
            {
                ValidateRegister(model);       
                _serviceRegister.SaveRegister(model);                
                return Json(new
                {
                    message = Constant.Message.Success.SAVE_SUCCESS,
                    success = true,
                    id = model.ID
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
        private void ValidateRegister(RegisterViewModel model)
        {
            if (string.IsNullOrEmpty(model.FirstName.ToStringNullable())                
                || model.RegisterTypeID.AsInt() == 0
                || string.IsNullOrEmpty(model.Email.ToStringNullable())
                || string.IsNullOrEmpty(model.Mobile.ToStringNullable())
                || string.IsNullOrEmpty(model.Password.ToStringNullable())
                || string.IsNullOrEmpty(model.ConfirmPassword.ToStringNullable()))
            {
                throw new Exception(Constant.Message.Error.PLEASE_INPUT_DATA);
            }
            else if (!model.Password.Equals(model.ConfirmPassword))
            {
                throw new Exception(Constant.Message.Error.PASSWORD_CONFIRM_PASSWORD_NOT_EQUAL);
            }
        }

        [HttpPost]
        public JsonResult GetProjectQuota(int ID)
        {
            try
            {
                var projects = GetMasterProjectLists(false);
                ViewBag.ProjectSelectList = projects;
                
                var model = _serviceRegister.GetProjectQuotaByID(ID);
                var html = RenderRazorViewtoString(this, "Partial_Quota_Modal", model);
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
        public JsonResult SaveProjectQuota(ProjectRegisterQuota model)
        {
            try
            {
                validateProjectQuota(model);
                _serviceRegister.SaveProjectQuota(model);
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
        private void validateProjectQuota(ProjectRegisterQuota model) {
            if (model.ID > 0)
            {
                if (model.ProjectID.AsGuid() == Guid.Empty)
                {
                    throw new Exception(Constant.Message.Error.PLEASE_INPUT_DATA);
                }
            }
            //if (model.Quota.AsInt() == 0)
            //    throw new Exception(Constant.Message.Error.PLEASE_INPUT_DATA);
            
        }
    }
}
