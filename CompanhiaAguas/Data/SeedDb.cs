using CompanhiaAguas.Data.Entities;
using CompanhiaAguas.Helpers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace CompanhiaAguas.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly RoleManager<IdentityRole> _roleManager;
        private Random _random;

        public SeedDb(DataContext context, IUserHelper userHelper, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userHelper = userHelper;
            _random = new Random();
            _roleManager = roleManager;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            var user = await _userHelper.GetUserByEmailAsync("luispatricio.info@gmail.com");
            if (user == null)
            {
                user = new User
                {
                    FirstName = "Luis",
                    LastName = "Patricio",
                    Email = "luispatricio.info@gmail.com",
                    UserName = "luispatricio.info@gmail.com",
                    PhoneNumber = "999666333"
                };

                var result = await _userHelper.AddUserAsync(user, "121212");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not Create User in Seeder");
                }
            }
        }
    }
}
