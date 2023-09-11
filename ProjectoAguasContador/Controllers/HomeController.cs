using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectoAguasContador.Data;
using ProjectoAguasContador.Data.Entities;
using ProjectoAguasContador.Helpers;
using ProjectoAguasContador.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectoAguasContador.Controllers
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
                return RedirectToAction("Dashboard");
            }

            return View();
        }

        public async Task<IActionResult> Dashboard()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index");
            }

            DashboardViewModel dashboard = new DashboardViewModel();

            if (User.IsInRole("Admin"))
            {
                dashboard.AdminDashboard = new AdminDashboardViewModel();
                dashboard.AdminDashboard.Customers = new List<User>();
                dashboard.AdminDashboard.Employees = new List<User>();

                foreach (var user in _dataContext.Users)
                {
                    if (user.EmployeeApproved && !user.AdminApproved && await _userHelper.IsUserInRoleAsync(user, "Customer"))
                    {
                        dashboard.AdminDashboard.Customers.Add(user);
                    }
                    
                    if (!user.AdminApproved && await _userHelper.IsUserInRoleAsync(user, "Employee"))
                    {
                        dashboard.AdminDashboard.Employees.Add(user);
                    }

                }
            }
            else if (User.IsInRole("Employee"))
            {
                dashboard.EmployeeDashboard = new EmployeeDashboardViewModel();
                dashboard.EmployeeDashboard.Customers = new List<User>();

                foreach (var user in _dataContext.Users)
                {
                    if (!user.EmployeeApproved && await _userHelper.IsUserInRoleAsync(user, "Customer"))
                    {
                        dashboard.EmployeeDashboard.Customers.Add(user);
                    }
                }

            }
            else if (User.IsInRole("Customer"))
            {

            }


            return View(dashboard);
        }

        public IActionResult Privacy()
        {
            return View();
        }


    }
}
