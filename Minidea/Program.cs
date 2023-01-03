using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Minidea.DAL;
using Minidea.Models;

IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Db_MinideaContext>(options =>
    options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("Default")));


builder.Services.AddControllersWithViews();

//var provider = builder.Services.BuildServiceProvider();
//var configuration=provider.GetRequiredService<IConfiguration>();


builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});


builder.Services.AddIdentity<AppUser, IdentityRole>(option =>
{
    option.Password.RequireDigit = true;
    option.Password.RequiredLength = 6;

    option.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<Db_MinideaContext>()
.AddDefaultUI()
.AddDefaultTokenProviders();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    //var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    //var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
    //var context = scope.ServiceProvider.GetRequiredService<Db_MinideaContext>();
    //await DefaultRoles.SeedAsync(userManager, roleManager);
    var services = scope.ServiceProvider;

    await DbInitializers.Seed(services); //context, userManager, roleManager, builder.Configuration .Wait();
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(routes =>
{
    routes.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Account}/{action=Login}/{id?}");

    routes.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});


app.Run();

