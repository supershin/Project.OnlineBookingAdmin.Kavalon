using Project.Booking.Admin.Extensions;
using Project.Booking.Admin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Project.Booking.Admin.Business.Extensions
{
    public static class CookieExtexsion
    {
        public static UserProfile GetUserProfile(this ClaimsPrincipal User)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userProfile = new UserProfile
                {
                    ID = new Guid(User.Claims.FirstOrDefault(e => e.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Value),
                    Username = User.Claims.FirstOrDefault(e => e.Type == System.Security.Claims.ClaimTypes.Name).Value,
                    DepartmentID = User.Claims.FirstOrDefault(e => e.Type == "DepartmentID").Value.ToInt().AsInt(),
                    RoleID = User.Claims.FirstOrDefault(e => e.Type == "RoleID").Value.ToInt().AsInt(),
                    RoleLevel = User.Claims.FirstOrDefault(e => e.Type == "RoleLevel").Value.ToInt().AsInt()
                };
                return userProfile;
            }
            else return null;
        }
    }
}
