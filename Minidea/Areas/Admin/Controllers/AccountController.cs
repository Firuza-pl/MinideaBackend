using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Minidea.DAL;
using Minidea.Models;
using Minidea.ViewModels;
using System;
using System.Threading.Tasks;

namespace Minidea.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly Db_MinideaContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _siginInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(Db_MinideaContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> siginInManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _siginInManager = siginInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) return View(loginViewModel);
            try
            {
                AppUser user = await _userManager.FindByEmailAsync(loginViewModel.Email);

                if (user == null)
                {
                    ModelState.AddModelError("", "Boş xanaları doldurun");
                    return View(loginViewModel);
                }

                Microsoft.AspNetCore.Identity.SignInResult signInResult = await _siginInManager.PasswordSignInAsync(user, loginViewModel.Password, true, true);

                if (!signInResult.Succeeded)
                {
                    ModelState.AddModelError("", "Şifrə yanlışdır");
                    return View(loginViewModel);
                }
            }
            catch (Exception e)
            {

                 e.Message.ToString();
            }


            return RedirectToAction("Load", "Dashboard");
        }


        public async Task<IActionResult> Logout()
        {
            await _siginInManager.SignOutAsync();
            //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }


    }
}
