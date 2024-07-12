using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Project.Booking.Admin.Business.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Booking.Admin.Web.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class IdentityAuthorize : Attribute, IAuthorizationFilter
    {
        public string Roles { get; set; }
        public string Departments { get; set; }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User.GetUserProfile();
            string[] roles = Roles.Split(new char[] { ',' });
            string[] departments = Departments.Split(new char[] { ',' });

            var url = $"{context.HttpContext.Request.Scheme}://{context.HttpContext.Request.Host}{context.HttpContext.Request.PathBase}";
            url = url.EndsWith("/") ? url : string.Concat(url, "/");

            if (!departments.Any(e => user.DepartmentID.ToString().Contains(e)))
                context.HttpContext.Response.Redirect($"{url}Error/AccessDenied");
            else
                if (!roles.Any(e => user.RoleID.ToString().Contains(e)))
                context.HttpContext.Response.Redirect($"{url}Error/AccessDenied");
        }
    }
}
