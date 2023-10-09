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
    public class DashboardController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _dataContext;
        private readonly IUserHelper _userHelper;

        public DashboardController(ILogger<HomeController> logger, DataContext dataContext, IUserHelper userHelper)
        {
            _logger = logger;
            _dataContext = dataContext;
            _userHelper = userHelper;
        }

        public async Task<IActionResult> Dashboard()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Home","Index");
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
    }
}
