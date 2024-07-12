using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project.Booking.Admin.Data.Models;
using Project.Booking.Admin.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Booking.Admin.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly OnlineBookingDbContext _dbContext;
        public HomeController(ILogger<HomeController> logger, OnlineBookingDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index(string returnUrl = null)
        {
            var unit = _dbContext.tm_Unit.ToList();
            return View(unit);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
