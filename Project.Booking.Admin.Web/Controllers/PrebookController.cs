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
    public class PrebookController : BaseController
    {                
        public PrebookController(
            IMaster serviceMaster,                         
             IHttpContextAccessor httpContextAccessor,
            IOptions<ApplicationSetting> config) : base(serviceMaster, config, httpContextAccessor)
        {
            
            
        }
        public IActionResult Index()
        {
            var projects = GetMasterProjectLists(false);
            ViewBag.ProjectSelectList = projects;
            ViewBag.SelectProjectID = GetCookie(CookieKey.General.ProjectID) ?? projects.FirstOrDefault().Value;
            return View();
        }
    }
}
