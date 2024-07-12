using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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
    public class UnitController : BaseController
    {
        private readonly IUnit _serviceUnit;
        public UnitController(IMaster serviceMaster,
                              IUnit serviceUnit,
                              IHttpContextAccessor httpContextAccessor,
                              IOptions<ApplicationSetting> config) : base(serviceMaster, config, httpContextAccessor)
        {
            _serviceUnit = serviceUnit;
        }
        public IActionResult Index()
        {
            var model = new List<UnitViewModel>();            
            var projects = GetMasterProjectLists(false);
            ViewBag.ProjectSelectList = projects;
            ViewBag.SelectProjectID = GetCookie(CookieKey.General.ProjectID)?? projects.FirstOrDefault().Value;

            return View(model);
        }
        public JsonResult GetUnitByProjectAll(Guid ProjectID)
        {
            try
            {
                SetCookie(CookieKey.General.ProjectID, ProjectID.ToString(), 30);
                var model = _serviceUnit.GetUnitByProjectAll(ProjectID);
                var html = RenderRazorViewtoString(this, "Partial_Unit_List", model);
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
            var model = _serviceUnit.GetUnitByID(ID.AsGuid());
            ViewBag.UnitStatusSelectList = GetMasterUnitStatusLists();
            return View(model);
        }
        [HttpPost]
        public IActionResult SaveUnit(UnitViewModel model)
        {
            ViewBag.UnitStatusSelectList = GetMasterUnitStatusLists();
            try
            {
                if (ModelState.IsValid)
                {
                    _serviceUnit.SaveUnit(model);
                    model = _serviceUnit.GetUnitByID(model.ID.AsGuid());
                    var html = RenderRazorViewtoString(this, "Partial_Unit_Form", model);
                    return Json(new
                    {
                        success = true,
                        unit = model,
                        message = Constant.Message.Success.SAVE_SUCCESS,
                        html = html
                    });
                }
                else
                {
                    model = _serviceUnit.GetUnitByID(model.ID.AsGuid());
                    return JsonPartialViewError("Partial_Unit_Form", model);
                }
            }
            catch (Exception ex)
            {
                model = _serviceUnit.GetUnitByID(model.ID.AsGuid());
                ViewBag.ErrorMsg = InnerException(ex);
                return JsonPartialViewError("Partial_Unit_Form", model, InnerException(ex));
            }
        }
    }
}
