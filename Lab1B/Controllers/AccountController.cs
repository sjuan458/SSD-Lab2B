using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab1B.Data;
using Lab1B.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


// I, Juan Salazar, student number 000734250, certify that this material is my
// original work. No other person's work has been used without due
// acknowledgement and I have not made my work available to anyone else.

namespace Lab1B.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> SeedRoles()
        {

            ApplicationUser user1 = new ApplicationUser
            {
                Email = "John@doe.com",
                UserName = "John@doe.com",
                BirthDate = DateTime.Parse("1990/04/16"),
                FirstName = "John",
                LastName = "Doe",
                Company = "Doe Ltd.",
                Position = "Staff"
            };

            ApplicationUser user2 = new ApplicationUser
            {
                Email = "Jane@doe.com",
                UserName = "Jane@doe.com",
                BirthDate = DateTime.Parse("1971/09/25"),
                FirstName = "Jane",
                LastName = "Doe",
                Company = "Doe Ltd.",
                Position = "Manager"
            };

            IdentityResult result = await _userManager.CreateAsync(user1, "Member1!");
            if (!result.Succeeded)
            {
                return View("Error", new ErrorViewModel { RequestId = "Failed to add new user" });
            }
            result = await _userManager.CreateAsync(user2, "Member2!");
            if (!result.Succeeded)
            {
                return View("Error", new ErrorViewModel { RequestId = "Failed to add new user" });
            }

            result = await _roleManager.CreateAsync(new IdentityRole("Staff"));
            if (!result.Succeeded)
            {
                return View("Error", new ErrorViewModel { RequestId = "Failed to add new role" });
            }

            result = await _roleManager.CreateAsync(new IdentityRole("Manager"));
            if (!result.Succeeded)
            {
                return View("Error", new ErrorViewModel { RequestId = "Failed to add new role" });
            }

            result = await _userManager.AddToRoleAsync(user1, "Staff");
            if (!result.Succeeded)
            {
                return View("Error", new ErrorViewModel { RequestId = "Failed to assign new role" });
            }

            result = await _userManager.AddToRoleAsync(user2, "Manager");
            if (!result.Succeeded)
            {
                return View("Error", new ErrorViewModel { RequestId = "Failed to assign new role" });
            }

            return View("../Home/Index");
        }
    }
}