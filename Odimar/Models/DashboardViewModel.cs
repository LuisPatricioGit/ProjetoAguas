using Odimar.Data.Entities;
using System.Collections.Generic;

namespace Odimar.Models
{
    public class DashboardViewModel
    {
        public AdminDashboardViewModel AdminDashboard { get; set; }
        public EmployeeDashboardViewModel EmployeeDashboard { get; set; }
        public CustomerDashboardViewModel CustomerDashboard { get; set; }

    }

    public class AdminDashboardViewModel
    {
        public List<User> Employees { get; set; }
        public List<User> Customers { get; set; }
        public User User { get; set; }
    }

    public class EmployeeDashboardViewModel
    {
        public List<User> Customers { get; set; }
        public User User { get; set; }
    }

    public class CustomerDashboardViewModel
    {
        
    }
}
