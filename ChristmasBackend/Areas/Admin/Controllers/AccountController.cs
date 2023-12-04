using ChristmasBackend.Areas.ViewModels.Account;
using ChristmasBackend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ChristmasBackend.Areas.Admin.Controllers
{
    
    public class AccountController : MainController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager,
                                 RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;

        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();


            List<UserVM> userVM = new();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                userVM.Add(new UserVM
                {
                    FullName = user.FullName,
                    Username = user.UserName,
                    Email = user.Email,
                    RoleName = roles
                });
            }

            return View(userVM);
        }

        [HttpGet]
        public async Task<IActionResult> AddRoleToUser()
        {
            ViewBag.roles = await GetRolesAsync();
            ViewBag.users = await GetUsersAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRoleToUser(UserRoleVM request)
        {
            AppUser user = await _userManager.FindByIdAsync(request.UserId);

            IdentityRole role = await _roleManager.FindByIdAsync(request.RoleId);

            await _userManager.AddToRoleAsync(user, role.Name);

            return RedirectToAction("Index");
        }

        private async Task<SelectList> GetRolesAsync()
        {
            return new SelectList(await _roleManager.Roles.ToListAsync(), "Id", "Name");
        }

        private async Task<SelectList> GetUsersAsync()
        {
            return new SelectList(await _userManager.Users.ToListAsync(), "Id", "UserName");
        }
    }
}
