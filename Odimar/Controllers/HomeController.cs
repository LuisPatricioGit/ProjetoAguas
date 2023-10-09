using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Odimar.Data;
using Odimar.Data.Entities;
using Odimar.Helpers;
using Odimar.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Odimar.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _dataContext;
        private readonly IUserHelper _userHelper;

        public HomeController(ILogger<HomeController> logger, DataContext dataContext, IUserHelper userHelper)
        {
            _logger = logger;
            _dataContext = dataContext;
            _userHelper = userHelper;
        }
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Dashboard", "Dashboard");
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
