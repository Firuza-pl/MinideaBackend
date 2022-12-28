using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Minidea.DAL;
using Minidea.Models;
using static System.Formats.Asn1.AsnWriter;
using IConfiguration = AutoMapper.IConfiguration;

var builder = WebApplication.CreateBuilder(args);

var provider = builder.Services.BuildServiceProvider();
//var configuration=provider.GetRequiredService<IConfiguration>();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddControllersWithViews(); 


builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});


builder.Services.AddDbContext<Db_MinideaContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:Default"]);
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

builder.Services.AddMvc();


var app = builder.Build();

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

//var scopeFactory =  provider.GetRequiredService<IServiceScopeFactory>();
//using (var scope = scopeFactory.CreateScope())
//{
//    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
//    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
//    var context = scope.ServiceProvider.GetRequiredService<Db_MinideaContext>();
//    //await DefaultRoles.SeedAsync(userManager, roleManager);

//    DbInitializers.Seed(context, userManager, roleManager, builder.Configuration).Wait();
//}

app.Run();

