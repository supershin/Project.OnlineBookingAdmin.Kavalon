using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Options;
using Project.Booking.Admin.Business.Extensions;
using Project.Booking.Admin.Business.Interfaces;
using Project.Booking.Admin.Configurations;
using Project.Booking.Admin.Constants;
using Project.Booking.Admin.Extensions;
using Project.Booking.Admin.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Booking.Admin.Web.Controllers
{
    public class BaseController : Controller
    {
        private readonly IMaster _serviceMaster;
        private readonly IOptions<ApplicationSetting> _config;
        private readonly IHttpContextAccessor _httpContextAccessor;
        protected readonly UserProfile userProfile;
        public string GetCookie(string key)
        {
            return Request.Cookies[key];
        }
        public void SetCookie(string key, string value, int? expireTime)
        {
            CookieOptions option = new CookieOptions();
            if (expireTime.HasValue)
                option.Expires = DateTime.Now.AddDays(expireTime.Value);
            else
                option.Expires = DateTime.Now.AddMilliseconds(10);
            Response.Cookies.Append(key, value, option);
        }

        protected string BaseUrl;
        public BaseController(IMaster serviceMaster,
            IOptions<ApplicationSetting> config,
            IHttpContextAccessor httpContextAccessor)
        {
            _serviceMaster = serviceMaster;
            _config = config;
            _httpContextAccessor = httpContextAccessor;
            this.userProfile = httpContextAccessor.HttpContext.User.GetUserProfile();
        }

        //protected UserProfile UserProfile
        //{
        //    get
        //    {
        //        return this.HttpContext.Session.Get<UserProfile>(SessionKey.Autentication.UserProfile);
        //    }
        //    set
        //    {
        //        this.HttpContext.Session.Set(SessionKey.Autentication.UserProfile, value);
        //    }
        //}

        [Microsoft.AspNetCore.Mvc.NonAction]
        public override void OnActionExecuting(Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext context)
        {
            var url = $"{context.HttpContext.Request.Scheme}://{context.HttpContext.Request.Host}{context.HttpContext.Request.PathBase}";
            url = url.EndsWith("/") ? url : string.Concat(url, "/");
            BaseUrl = url;
            ViewBag.baseUrl = BaseUrl;
            ViewBag.signalRUrl = _config.Value.SignalRConfig.Url;

        }
        #region properties
        //protected UserProfile UserProfile
        //{
        //    get
        //    {
        //        if (this.HttpContext.Items[SessionKey.Autentication.UserProfile] == null)
        //        {
        //            return null;
        //        }
        //        else
        //        {
        //            return (UserProfile)this.HttpContext.Items[SessionKey.Autentication.UserProfile];
        //        }
        //    }
        //    set
        //    {
        //        this.HttpContext.Items[SessionKey.Autentication.UserProfile] = value;
        //    }
        //}
        #endregion


        #region *** Master Select List ***
        public List<SelectListItem> GetMasterExtLists(int ExtTypeID, bool NoInput = true)
        {
            List<SelectListItem> temp = new List<SelectListItem>();
            foreach (var item in _serviceMaster.GetMasterExt(ExtTypeID))
            {
                temp.Add(new SelectListItem { Value = item.ID.ToString(), Text = item.Name });
            }
            if (NoInput)
            {
                temp.Insert(0, new SelectListItem { Value = "", Text = Constant.Ext.PLEASE_SELECT_ITEM });
            }
            return temp.ToList();
        }
        public List<SelectListItem> GetMasterDepartmentLists(bool NoInput = true)
        {
            List<SelectListItem> temp = new List<SelectListItem>();
            foreach (var item in _serviceMaster.GetMasterDepartment())
            {
                temp.Add(new SelectListItem { Value = item.ID.ToString(), Text = item.Name });
            }
            if (NoInput)
            {
                temp.Insert(0, new SelectListItem { Value = "", Text = Constant.Ext.PLEASE_SELECT_ITEM });
            }
            return temp.ToList();
        }
        public List<SelectListItem> GetMasterRoleLists(bool NoInput = true)
        {
            List<SelectListItem> temp = new List<SelectListItem>();
            foreach (var item in _serviceMaster.GetMasterRole())
            {
                temp.Add(new SelectListItem { Value = item.ID.ToString(), Text = item.Name });
            }
            if (NoInput)
            {
                temp.Insert(0, new SelectListItem { Value = "", Text = Constant.Ext.PLEASE_SELECT_ITEM });
            }
            return temp.ToList();
        }
        public List<SelectListItem> GetMasterProjectLists(bool NoInput = true)
        {
            List<SelectListItem> temp = new List<SelectListItem>();
            foreach (var item in _serviceMaster.GetMasterProject(userProfile.DepartmentID.AsInt()))
            {
                temp.Add(new SelectListItem { Value = item.ProjectID.ToString(), Text = item.ProjectName });
            }
            if (NoInput)
            {
                temp.Insert(0, new SelectListItem { Value = "", Text = Constant.Ext.PLEASE_SELECT_ITEM });
            }
            return temp.ToList();
        }
        public List<SelectListItem> GetMasterUnitStatusLists(bool NoInput = true)
        {
            List<SelectListItem> temp = new List<SelectListItem>();
            foreach (var item in _serviceMaster.GetMasterUnitStatus())
            {
                temp.Add(new SelectListItem { Value = item.ID.ToString(), Text = item.Name });
            }
            if (NoInput)
            {
                temp.Insert(0, new SelectListItem { Value = "", Text = Constant.Ext.PLEASE_SELECT_ITEM });
            }
            return temp.ToList();
        }
        public List<SelectListItem> GetMasterBookingStatusLists(bool NoInput = true)
        {
            List<SelectListItem> temp = new List<SelectListItem>();
            foreach (var item in _serviceMaster.GetMasterBookingStatus())
            {
                temp.Add(new SelectListItem { Value = item.ID.ToString(), Text = item.Name });
            }
            if (NoInput)
            {
                temp.Insert(0, new SelectListItem { Value = "", Text = Constant.Ext.PLEASE_SELECT_ITEM });
            }
            return temp.ToList();
        }
        public List<SelectListItem> GetMasterPaymentVerifyStatusLists(bool NoInput = true)
        {
            List<SelectListItem> temp = new List<SelectListItem>();
            foreach (var item in _serviceMaster.GetMasterExt(Constant.ExtType.PAYMENT_VERIFY_STATUS))
            {
                temp.Add(new SelectListItem { Value = item.ID.ToString(), Text = item.Name });
            }
            if (NoInput)
            {
                temp.Insert(0, new SelectListItem { Value = "", Text = Constant.Ext.SELECT_ALL });
            }
            return temp.ToList();
        }
        #endregion

        #region protected function
        protected string InnerException(Exception ex)
        {
            return (ex.InnerException != null) ? InnerException(ex.InnerException) : ex.Message;
        }
        protected string RenderRazorViewtoString(Controller controller, string viewName, object model = null)
        {
            controller.ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                IViewEngine viewEngine = controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
                ViewEngineResult viewEngineResult = viewEngine.FindView(controller.ControllerContext, viewName, false);
                ViewContext viewContext = new ViewContext
                (
                    controller.ControllerContext,
                    viewEngineResult.View,
                    controller.ViewData,
                    controller.TempData,
                    sw,
                    new HtmlHelperOptions()
                );
                viewEngineResult.View.RenderAsync(viewContext);
                return sw.GetStringBuilder().ToString();
            }
        }
        protected JsonResult JsonPartialViewError(string partial, object model, string message = "")
        {
            return Json(new
            {
                success = false,
                message = message,
                html = RenderRazorViewtoString(this, partial, model)
            });
        }
        #endregion

        #region private function        
        #endregion
    }
}
