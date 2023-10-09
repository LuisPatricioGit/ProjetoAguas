using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Odimar.Data.Entities;
using Odimar.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Odimar.Data
{
    public class SeedDB
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly RoleManager<IdentityRole> _roleManager;
        private Random _random;
        public SeedDB(DataContext context, IUserHelper userHelper, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userHelper = userHelper;
            _random = new Random();
            _roleManager = roleManager;
        }

        public async Task SeedAsync()
        {
            //await _context.Database.MigrateAsync();
            await _context.Database.EnsureCreatedAsync();

            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("Employee");
            await _userHelper.CheckRoleAsync("Client");

            if (!_context.Districts.Any())
            {
                var district = new District
                {
                    Name = "Lisboa",
                    Counties = new List<County>
                    {
                        new County
                        {
                            Name = "Odivelas",
                            Parishes = new List<Parish>
                            {
                                new Parish { Name = "Caneças e Ramada" },
                                new Parish { Name = "Famões e Pontinha" },
                                new Parish { Name = "Odivelas" }
                            }
                        },
                        new County { Name = "Amadora" },
                        new County { Name = "Loures" }
                    }
                };

                _context.Districts.Add(district);

                await _context.SaveChangesAsync();
            }

            // Creates Admin
            var user = await _userHelper.GetUserByEmailAsync("luispatricio.info@gmail.com");
            if (user == null)
            {
                user = new User
                {
                    FirstName = "Luis",
                    LastName = "Patricio",
                    Email = "luispatricio.info@gmail.com",
                    UserName = "luispatricio.info@gmail.com",
                    PhoneNumber = "96123321",
                    Address = "Rua Verdadeiramente Falsa",
                    CountyId = _context.Districts.FirstOrDefault().Counties.FirstOrDefault().Id,
                    County = _context.Districts.FirstOrDefault().Counties.FirstOrDefault(),
                    ParishId = _context.Counties.FirstOrDefault().Parishes.FirstOrDefault().Id,
                    Parish = _context.Counties.FirstOrDefault().Parishes.FirstOrDefault(),
                    EmployeeApproved = true,
                    AdminApproved = true
                };

                var result = await _userHelper.AddUserAsync(user, "121212");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user  in seeder");
                }

                await _userHelper.AddUserToRoleAsync(user, "Admin");
                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);
            }
            var isInRole = await _userHelper.IsUserInRoleAsync(user, "Admin");
            if (!isInRole)
            {
                await _userHelper.AddUserToRoleAsync(user, "Admin");
            }

            //Creates Employee
            user = await _userHelper.GetUserByEmailAsync("randomEmployee.info@gmail.com");
            if (user == null)
            {
                user = new User
                {
                    FirstName = "Mister",
                    LastName = "Employee",
                    Email = "randomEmployee.info@gmail.com",
                    UserName = "randomEmployee.info@gmail.com",
                    PhoneNumber = "321123456",
                    Address = "Rua Ua 3",
                    CountyId = _context.Districts.FirstOrDefault().Counties.FirstOrDefault().Id,
                    County = _context.Districts.FirstOrDefault().Counties.FirstOrDefault(),
                    ParishId = _context.Counties.FirstOrDefault().Parishes.FirstOrDefault().Id,
                    Parish = _context.Counties.FirstOrDefault().Parishes.FirstOrDefault(),
                    EmployeeApproved = true,
                    AdminApproved = true
                };

                var result = await _userHelper.AddUserAsync(user, "123456");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not Create User in Seeder");
                }

                await _userHelper.AddUserToRoleAsync(user, "Employee");
                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);
            }

            isInRole = await _userHelper.IsUserInRoleAsync(user, "Employee");
            if (!isInRole)
            {
                await _userHelper.AddUserToRoleAsync(user, "Employee");
            }

            if (!_context.Products.Any())
            {
                AddProduct("Iphone X", user);
                AddProduct("Magic Mause", user);
                AddProduct("Iwatch", user);
                AddProduct("Ipad mini", user);
            }

            await _context.SaveChangesAsync();
        }

        private void AddProduct(string name, User user)
        {
            _context.Products.Add(new Product
            {
                Name = name,
                Price = _random.Next(1000),
                IsAvailable = true,
                Stock = _random.Next(100),
                User = user
            });
        }
    }
}
