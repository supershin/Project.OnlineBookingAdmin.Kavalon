using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Project.Booking.Admin.Constants;
using System.Threading.Tasks;

namespace Project.Booking.Admin.Web.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            var path = httpContext.Request.Path;
            //path = (path.Value == "/") ? "" : path.Value;

            if (path.Value.StartsWith("/Dashboard"))
            {
                if (httpContext.Session.GetString(SessionKey.Autentication.UserProfile) == null)
                {
                    //var url = string.IsNullOrEmpty(path)? $"/Acount/"
                    //if (string.IsNullOrEmpty(path))
                    //{

                    //}
                    //httpContext.Response.Redirect("/Account/Index?returnUrl=" + path);
                    httpContext.Response.Redirect("/Account/Index");
                }
            }

            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class AuthenticationMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthenticationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthenticationMiddleware>();
        }
    }
}
