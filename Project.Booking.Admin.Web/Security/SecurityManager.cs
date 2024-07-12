using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Project.Booking.Admin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace Project.Booking.Admin.Web.Security
{
    public class SecurityManager
    {
        public async void SignIn(HttpContext httpContext, UserProfile userProfile)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,Convert.ToString(userProfile.ID)),
                new Claim(ClaimTypes.Name,Convert.ToString(userProfile.Username)),
                new Claim(ClaimTypes.Role,Convert.ToString(userProfile.RoleName)),
                new Claim("RoleID",Convert.ToString(userProfile.RoleID)),
                new Claim("DepartmentID",Convert.ToString(userProfile.DepartmentID)),
                new Claim("RoleLevel",Convert.ToString(userProfile.RoleLevel))
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                principal, new AuthenticationProperties
                {
                    //IsPersistent = userProfile.RememberLogin
                    IsPersistent = true
                    //IsPersistent = false
                });

        }

        public async void SignOut(HttpContext httpContext)
        {
            await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        private IEnumerable<Claim> getAccoutClaims(UserProfile userProfile)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, userProfile.Email));
            claims.Add(new Claim(ClaimTypes.Email, userProfile.Email));
            claims.Add(new Claim(ClaimTypes.Role, userProfile.RoleName));
            claims.Add(new Claim("RoleID", userProfile.RoleID.ToString()));
            claims.Add(new Claim("DepartmentID", userProfile.DepartmentID.ToString()));
            claims.Add(new Claim("RoleLevel", userProfile.RoleLevel.ToString()));
            return claims;
        }

        public string GetUserName(IPrincipal user)
        {
            var identity = (ClaimsIdentity)user.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            return claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value;

        }

    }
}
