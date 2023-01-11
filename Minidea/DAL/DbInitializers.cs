using Minidea.Models;
using Microsoft.AspNetCore.Identity;
using Minidea.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Minidea.DAL
{
    public class DbInitializers
    {   
        public async static Task Seed(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<Db_MinideaContext>();
            //await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();


            var user = new AppUser
            {
                Firstname = "ADMIN",
                Lastname = "ADMIN",
                Email = "firuza.p@qcs.az",
                NormalizedEmail = "FIRUZA.P@QCS.AZ",
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                PhoneNumber = "+994775542101",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };


            var staticData = new StaticData
            {
                PhoneOne = "+994775542101",
                MobileTwo = "+994775542101",
                MobileThree = "+994775542101",
                Email = "firuza.p@qcs.az",
                FacebookLink = "FB",
                InstagramLink = "INS",
                LinkedinLink = "LINK",
                PhotoURL = "/staticdata/logo-transparent",
                IsActive = true
            };

            var back = new BackgroundImages
            {
                PhotoURL = "/backgroundImages/background.jpg",
                IsActive = true
            };


#pragma warning disable CS8602 // Dereference of a possibly null reference.
            if (!context.Users.Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<AppUser>();
                var hashed = password.HashPassword(user, "Admin123!");
                user.PasswordHash = hashed;

                var userStore = new UserStore<AppUser>(context);
                var result = userStore.CreateAsync(user);

                IList<StaticData> staticDatas = new List<StaticData>();
                staticDatas.Add(staticData);
                context.StaticDatas.AddRange(staticDatas);

                IList<BackgroundImages> backgroundImages = new List<BackgroundImages>();
                backgroundImages.Add(back);
                context.BackgroundImages.AddRange(backgroundImages);
            }

#pragma warning restore CS8602 // Dereference of a possibly null reference.
            await context.SaveChangesAsync();

        }
    }
}
