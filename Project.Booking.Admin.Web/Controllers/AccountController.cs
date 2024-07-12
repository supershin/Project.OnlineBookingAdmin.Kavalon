using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Project.Booking.Admin.Business.Interfaces;
using Project.Booking.Admin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Booking.Admin.Web.Security;
using Project.Booking.Admin.Constants;
using Project.Booking.Admin.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Project.Booking.Admin.Configurations;

namespace Project.Booking.Admin.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IMaster _serviceMaster;
        private readonly IUser _serviceUser;
        private readonly SecurityManager securityManager = new SecurityManager();
        public AccountController(IMaster serviceMaster, 
                                 IUser serviceUser,
                                 IHttpContextAccessor httpContextAccessor,
                                 IOptions<ApplicationSetting> config) : base(serviceMaster,config, httpContextAccessor)
        {
            _serviceMaster = serviceMaster;
            _serviceUser = serviceUser;
        }
        public IActionResult Index(string returnUrl = "")
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public IActionResult Index(UserProfile userProfile, string returnUrl)
        {
            try
            {
                validateLogin(userProfile.Username, userProfile.Password);

                var user = _serviceUser.VerifyLogin(userProfile.Username, userProfile.Password);

                securityManager.SignIn(this.HttpContext, user);

                //base.UserProfile = user;

                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    var url = returnUrl.Substring(1, returnUrl.Length - 1);
                    //return Redirect(BaseUrl + url);
                    return LocalRedirect(returnUrl);
                }
                else return RedirectToAction("Index", "Dashboard");
            }
            catch (Exception ex)
            {
                ViewBag.returnUrl = returnUrl;
                ViewBag.ErrorMsg = InnerException(ex);
                return View("Index", userProfile);
            }
        }
        public IActionResult Logout()
        {
            securityManager.SignOut(this.HttpContext);
            //HttpContext.Session.Remove(SessionKey.Autentication.UserProfile);
            //HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        public IActionResult AccessDenied()
        {           
            return View();
        }
        private void validateLogin(string username, string password)
        {
            if (string.IsNullOrEmpty(username.ToStringNullable()) || string.IsNullOrEmpty(password))
            {
                throw new Exception(Constant.Message.Error.INVALID_LOGIN);
            }
        }
    }
}
