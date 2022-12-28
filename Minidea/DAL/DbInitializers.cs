using Minidea.Models;
using Microsoft.AspNetCore.Identity;
using Minidea.Utilities;

namespace Minidea.DAL
{
    public class DbInitializers
    {
        public async static Task Seed(Db_MinideaContext _context, UserManager<AppUser> userManager,
                                                                    RoleManager<IdentityRole> roleManager,
                                                                    IConfiguration configuration)
        {
            await _context.Database.EnsureCreatedAsync();
            if (!await roleManager.RoleExistsAsync(StaticUsers.Admin))
            {
                await roleManager.CreateAsync(new IdentityRole(StaticUsers.Admin));
            }

            if (!await roleManager.RoleExistsAsync(StaticUsers.Member))
            {
                await roleManager.CreateAsync(new IdentityRole(StaticUsers.Member));
            }

            if (await userManager.FindByNameAsync("Admin") == null)
            {
                var admin = new AppUser()
                {
                    Firstname = "Admin",
                    Lastname = "Admin",
                    Email = "rustemlifiruze@gmail.com",
                    UserName = "admin",
                };

                var result = await userManager.CreateAsync(admin, "Admin123!");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, StaticUsers.Admin);
                }
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, StaticUsers.Admin);
                }
            }
        }
    }
}
