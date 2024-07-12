using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Project.Booking.Admin.Business.Interfaces;
using Project.Booking.Admin.Configurations;
using Project.Booking.Admin.Constants;
using Project.Booking.Admin.Extensions;
using Project.Booking.Admin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Booking.Admin.Web.Controllers
{
    [Authorize]
    public class MatrixController : BaseController
    {
        private readonly IUnit _serviceUnit;
        public MatrixController(
             IUnit serviceUnit,
             IMaster serviceMaster,
             IHttpContextAccessor httpContextAccessor,
             IOptions<ApplicationSetting> config) : base(serviceMaster, config, httpContextAccessor)
        {
            _serviceUnit = serviceUnit;
        }
        public ActionResult Index(Guid projectID, string builds)
        {
            var model = _serviceUnit.GetUnitMatrix(projectID, builds);
            return View(model);
        }
        public ActionResult Pre(Guid projectID)
        {
            var builds = _serviceUnit.GetBuilds(projectID);
            return RedirectToAction("Index", new { projectID, builds });
        }

        [HttpPost]
        public IActionResult GetUnit(Guid ID)
        {            
            try
            {
                var unit = _serviceUnit.GetUnitByID(ID);
                var html = RenderRazorViewtoString(this, "Partial_Unit_Modal", unit);
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
        public IActionResult SaveUnitBooking(UnitViewModel model)
        {
            try
            {
                model.UnitStatusID = Constant.UnitStatus.BOOKING;
                _serviceUnit.SaveUnit(model);
                model = _serviceUnit.GetUnitByID(model.ID.AsGuid());                            
                return Json(new
                {                                        
                    success = true,
                    unit = model,
                    message = Constant.Message.Success.SAVE_SUCCESS
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
    }
}
