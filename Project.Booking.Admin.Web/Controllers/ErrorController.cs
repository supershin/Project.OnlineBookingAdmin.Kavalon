using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Booking.Admin.Web.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/AccessDenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry, the resource you requested could not be bound";
                    break;
                case 500:
                    ViewBag.ErrorMessage = "Sorry, Internal Server Error";
                    break;
                default:
                    break;
            }
            ViewBag.StatusCode = statusCode;
            return View("Error");
        }

        [Route("Error/InternalError")]
        public IActionResult InternalError()
        {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            ViewBag.ExceptionPath = exceptionDetails.Path;
            ViewBag.ExceptionMessage = exceptionDetails.Error.Message;
            ViewBag.Stacktrace = exceptionDetails.Error.StackTrace;
            ViewBag.ErrorMessage = "Sorry, Internal Server Error";
            ViewBag.StatusCode = 500;
            return View("Error");
        }
    }
}
