using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Project.Booking.Admin.Business.Interfaces;
using Project.Booking.Admin.Business.Services;
using Project.Booking.Admin.Configurations;
using Project.Booking.Admin.Constants;
using Project.Booking.Admin.Data.Models;
using Project.Booking.Admin.Web.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Booking.Admin.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(option =>
                {
                    option.LoginPath = "/Account/Index";
                    option.Cookie.Name = CookieKey.Autentication.UserProfile;
                    option.ExpireTimeSpan = TimeSpan.FromDays(30);
                });

            services.Configure<ApplicationSetting>(Configuration.GetSection("ApplicationSettings"));

            services.AddControllers().AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);

            //DbContext Configuration
            services.AddDbContext<OnlineBookingDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("OnllineBookingContext")));

            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            services.AddScoped<IMaster, MasterService>();
            services.AddScoped<IUser, UserService>();
            services.AddScoped<IRegister, RegisterService>();
            services.AddScoped<IUnit, UnitService>();
            services.AddScoped<IBooking, BookingService>();
            services.AddScoped<IReport, ReportService>();
            services.AddScoped<IDashboard, DashboardService>();
            services.AddScoped<ITransferPayment, TransferPaymentService>();

            services.AddDistributedMemoryCache();

            //services.AddSession(options =>
            //{
            //    options.IdleTimeout = TimeSpan.FromMinutes(360);
            //    options.Cookie.HttpOnly = true;
            //    options.Cookie.IsEssential = true;
            //});

            services.AddHttpContextAccessor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();                
            }           
            else
            {
                app.UseExceptionHandler("/Error/InternalError");
                app.UseStatusCodePagesWithRedirects("~/Error/{0}");
                //app.UseStatusCodePagesWithReExecute("~/Error/{0}");

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //app.UseSession();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseCookiePolicy();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    //pattern: "{controller=Account}/{action=Index}/{id?}");
                    pattern: "{controller=Dashboard}/{action=Index}");
            });
        }
    }
}
