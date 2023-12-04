using ChristmasBackend.Helpers.Enums;
using ChristmasBackend.Models;
using ChristmasBackend.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ChristmasBackend.Controllers
{
	public class AccountController : Controller
	{
		private readonly SignInManager<AppUser> _signInManager;
		private readonly UserManager<AppUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		public AccountController(UserManager<AppUser> userManager,
								SignInManager<AppUser> signInManager,
								RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_roleManager = roleManager;
		}

		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }


            AppUser user = new()
            {
                FullName = request.FullName,
                UserName = request.UserName,
                Email = request.Email
            };

            IdentityResult result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }
                return View(request);
            }
            var createdUser = await _userManager.FindByNameAsync(user.UserName);

            await _userManager.AddToRoleAsync(createdUser, Roles.Member.ToString());


            return RedirectToAction("Index", "Home");
        }


        //[HttpGet]
        //public async Task<IActionResult> CreateRoles()
        //{
        //    foreach (var role in Enum.GetValues(typeof(Roles)))
        //    {
        //        if (!await _roleManager.RoleExistsAsync(role.ToString()))
        //        {
        //            await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
        //        }
        //    }
        //    return Ok();
        //}


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);

            }

            AppUser user = await _userManager.FindByEmailAsync(request.Email);


            if (user is null)
            {
                ModelState.AddModelError(string.Empty, "Email or password is wrong");
                return View(request);
            }

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);


            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Email or password is wrong");
                return View(request);
            }


            return RedirectToAction("Index", "Home");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
