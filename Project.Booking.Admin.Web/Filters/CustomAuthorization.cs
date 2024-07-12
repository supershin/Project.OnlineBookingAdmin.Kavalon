using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Project.Booking.Admin.Business.Interfaces;
using Project.Booking.Admin.Business.Services;
using Project.Booking.Admin.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace Project.Booking.Admin.Web.Filters
{
    public class CustomAuthorization : AuthorizeAttribute, IAuthorizationFilter
    {
        public string Department { get; set; }

        private readonly IUser _serviceUser = new UserService(new OnlineBookingDbContext());
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string username = GetUserName(context.HttpContext.User);
            if (string.IsNullOrEmpty(username))
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new
                    {
                        Controller = "Account",
                        Action = "index",
                        returnUrl = context.HttpContext.Request.Path
                    }
                    ));
            }
            else
            {
                var user = _serviceUser.GetUserProfile(username);
                context.HttpContext.Items["UserProfile"] = user;
            }
        }
        public string GetUserName(IPrincipal user)
        {
            var identity = (ClaimsIdentity)user.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            return claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value;

        }
    }
}
