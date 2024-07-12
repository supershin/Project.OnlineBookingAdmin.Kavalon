using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Project.Booking.Admin.Business.Extensions;
using Project.Booking.Admin.Business.Interfaces;
using Project.Booking.Admin.Configurations;
using Project.Booking.Admin.Constants;
using Project.Booking.Admin.Extensions;
using Project.Booking.Admin.Model;
using Project.Booking.Admin.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Project.Booking.Admin.Web.Controllers
{
    //[CustomAuthenticationFilter]
    [Authorize]
    public class DashboardController : BaseController
    {
        private readonly IMaster _serviceMaster;
        private readonly IHttpContextAccessor _httpContextAccessor;        
        private readonly UserProfile userProfile;
        private readonly IDashboard serviceDasboard;

        public DashboardController(IMaster serviceMaster,
            IDashboard serviceDasboard,
            IHttpContextAccessor httpContextAccessor,
            IOptions<ApplicationSetting> config) :base(serviceMaster, config, httpContextAccessor)
        {
            _serviceMaster = serviceMaster;
            _httpContextAccessor = httpContextAccessor;
            this.serviceDasboard = serviceDasboard;
            this.userProfile = httpContextAccessor.HttpContext.User.GetUserProfile();
        }

        public IActionResult Index()
        {
            var model = new DashboardViewModel();
            model.BUList = _serviceMaster.GetMasterBU(userProfile.DepartmentID.AsInt());
            model.ProjectList = _serviceMaster.GetMasterProject(userProfile.DepartmentID.AsInt())
                                .Where(e => e.BUID == Convert.ToInt32(model.BUList.FirstOrDefault().ID)).ToList();
            model.BUIDs = model.BUList.FirstOrDefault().ID.ToString();
            serviceDasboard.GetData(model);
            return View(model);
        }

        [HttpPost]
        public JsonResult GetProjectByBU(List<string> BUIDs)
        {
            try
            {

                var buIDs = string.Join(",", BUIDs);
                var data = _serviceMaster.GetMasterProjectByBU(buIDs);

                //return Json(CityList, JsonRequestBehavior.AllowGet);
                var json = from p in data
                           select new
                           {
                               projectID = p.ProjectID,
                               projectName = p.ProjectName
                           };

                return Json(new
                {
                    success = true,
                    data = json
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
        public JsonResult GetData(DashboardViewModel model)
        {
            try
            {
                validateData(model);
                serviceDasboard.GetData(model);
                var section1 = RenderRazorViewtoString(this, "Partial_Section_1", model);
                return Json(new
                {
                    success = true,
                    section1
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
        private void validateData(DashboardViewModel criteria)
        {
            if (string.IsNullOrEmpty(criteria.BUIDs.ToStringNullable()))
                throw new Exception(Constant.Message.Error.PLEASE_INPUT_SELECT_BU);
        }
    }
}
