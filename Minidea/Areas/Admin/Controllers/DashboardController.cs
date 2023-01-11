using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Minidea.DAL;
using Minidea.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Minidea.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class DashboardController : Controller
    {

        private readonly Db_MinideaContext _context;
        private UserManager<AppUser> _userManager;

        public DashboardController(Db_MinideaContext context,
                                   UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Load()
        {
            //ClaimsPrincipal currentUser = HttpContext.User;
            //var user = _userManager.GetUserAsync(User).Result;

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }
    }
}
