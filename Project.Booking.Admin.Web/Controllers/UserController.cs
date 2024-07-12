using Microsoft.AspNetCore.Mvc;
using Project.Booking.Admin.Model;
using Project.Booking.Admin.Extensions;
using Project.Booking.Admin.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Booking.Admin.Business.Interfaces;
using Project.Booking.Admin.Web.Filters;
using Microsoft.Extensions.Options;
using Project.Booking.Admin.Configurations;
using Microsoft.AspNetCore.Authorization;
using Project.Booking.Admin.Web.Security;
using Microsoft.AspNetCore.Http;

namespace Project.Booking.Admin.Web.Controllers
{
    //[CustomAuthenticationFilter]
    [Authorize]
    public class UserController : BaseController
    {
        private readonly SecurityManager securityManager = new SecurityManager();
        private readonly IMaster _serviceMaster;
        private readonly IUser _serviceUser;
        public UserController(IMaster serviceMaster,
                                IUser serviceUser,
                                IHttpContextAccessor httpContextAccessor,
                                IOptions<ApplicationSetting> config) : base(serviceMaster, config, httpContextAccessor)
        {
            _serviceMaster = serviceMaster;
            _serviceUser = serviceUser;
        }

        #region User        
        [IdentityAuthorize(Departments ="1",Roles ="1")]
        public IActionResult Index()
        {
            var model = _serviceUser.GetUserAll();
            return View(model);
        }
        [IdentityAuthorize(Departments = "1", Roles = "1")]
        public IActionResult Manage(Guid? ID = null)
        {
            ViewBag.DepartmentSelectList = GetMasterDepartmentLists();
            ViewBag.RoleSelectList = GetMasterRoleLists();
            var model = _serviceUser.GetUserByID(ID.AsGuid());
            return View(model);
        }
        [HttpPost]
        public JsonResult SaveUser(UserProfile model)
        {
            try
            {
                ValidateUser(model);
                model.isUserProfile = false;
                _serviceUser.SaveUser(model);
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
        private void ValidateUser(UserProfile model)
        {
            if (string.IsNullOrEmpty(model.FirstName.ToStringNullable())
                || string.IsNullOrEmpty(model.LastName.ToStringNullable())
                || string.IsNullOrEmpty(model.Username.ToStringNullable())
                || string.IsNullOrEmpty(model.Mobile.ToStringNullable())
                || model.DepartmentID.AsInt() == 0
                || model.RoleID.AsInt() == 0)
            {
                throw new Exception(Constant.Message.Error.PLEASE_INPUT_DATA);
            }
            else if (model.ID == Guid.Empty)
            {
                if (string.IsNullOrEmpty(model.Password) || string.IsNullOrEmpty(model.ConfirmPassword))
                {
                    throw new Exception(Constant.Message.Error.PLEASE_INPUT_DATA);
                }
                else if (!model.Password.Equals(model.ConfirmPassword))
                {
                    throw new Exception(Constant.Message.Error.PASSWORD_CONFIRM_PASSWORD_NOT_EQUAL);
                }
            }
            else if (model.ID != Guid.Empty)
            {
                if (model.Password != model.ConfirmPassword)
                    throw new Exception(Constant.Message.Error.PASSWORD_CONFIRM_PASSWORD_NOT_EQUAL);
            }
        }
        #endregion

        #region User Profile        
        public IActionResult ManageProfile(Guid ID)
        {
            var model = _serviceUser.GetUserByID(ID);
            return View(model);
        }
        [HttpPost]
        public JsonResult SaveUserProfile(UserProfile model)
        {
            try
            {
                ValidateUserProfile(model);
                model.isUserProfile = true;
                _serviceUser.SaveUser(model);
                securityManager.SignOut(this.HttpContext);
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
        private void ValidateUserProfile(UserProfile model)
        {
            if (string.IsNullOrEmpty(model.FirstName.ToStringNullable())
                || string.IsNullOrEmpty(model.LastName.ToStringNullable())
                || string.IsNullOrEmpty(model.Username.ToStringNullable())
                || string.IsNullOrEmpty(model.Mobile.ToStringNullable()))
            {
                throw new Exception(Constant.Message.Error.PLEASE_INPUT_DATA);
            }
            else if (model.Password != model.ConfirmPassword)
            {
                throw new Exception(Constant.Message.Error.PASSWORD_CONFIRM_PASSWORD_NOT_EQUAL);
            }
        }
        #endregion
    }
}
