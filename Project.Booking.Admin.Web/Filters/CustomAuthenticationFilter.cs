using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Core;
using Microsoft.AspNetCore.Mvc;
using Project.Booking.Admin.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http.Extensions;

namespace Project.Booking.Admin.Web.Filters
{
    public class CustomAuthenticationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {

            if (context.HttpContext.Session.GetString(SessionKey.Autentication.UserProfile) == null)
            {
                //Redirecting the user to the Login View of Account Controller  
                context.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                {
                         { "controller", "Account" },
                         { "action", "Index" },
                          { "returnUrl",context.HttpContext.Request.Path}
                });
                var rawUrl = Microsoft.AspNetCore.Http.Extensions.UriHelper.GetDisplayUrl(context.HttpContext.Request);

                var isAjax = context.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
                if (!isAjax)
                {
                    //Redirecting the user to the Login View of Account Controller  
                    context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                         { "controller", "Account" },
                         { "action", "Index" },
                          { "returnUrl",context.HttpContext.Request.Path}
                    });
                }
                else
                {
                    context.Result = new JsonResult(new
                    {
                        Data = new
                        {
                            // put whatever data you want which will be sent
                            // to the client
                            success = false,
                            message = Constant.Message.Error.SESSION_TIME_OUT,
                            sessionTimeOut = true,
                            returnUrl = context.HttpContext.Request.Path
                        },
                        JsonRequestBehavior = new Newtonsoft.Json.JsonSerializerSettings()
                    });
                }
            }
        }

        //public override void OnActionExecuted(ActionExecutedContext context)
        //{
        //    if (context.Result == null || context.Result is UnauthorizedResult)
        //    {
        //        //var rawUrl = Microsoft.AspNetCore.Http.Extensions.UriHelper.GetDisplayUrl(context.HttpContext.Request);

        //        var isAjax = context.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
        //        if (isAjax)
        //        {
        //            context.Result = new JsonResult(new
        //            {
        //                Data = new
        //                {
        //                    // put whatever data you want which will be sent
        //                    // to the client
        //                    success = false,
        //                    message = Constant.Message.Error.SESSION_TIME_OUT,
        //                    sessionTimeOut = true,
        //                    returnUrl = context.HttpContext.Request.Path
        //                },
        //                JsonRequestBehavior = new Newtonsoft.Json.JsonSerializerSettings()
        //            });
        //        }
        //        else
        //        {
        //            //Redirecting the user to the Login View of Account Controller  
        //            context.Result = new RedirectToRouteResult(
        //            new RouteValueDictionary
        //            {
        //                 { "controller", "Account" },
        //                 { "action", "Index" },
        //                  { "returnUrl",context.HttpContext.Request.Path}
        //            });
        //        }
        //    }
        //    //base.OnActionExecuted(context); 
        //}

    }
}
