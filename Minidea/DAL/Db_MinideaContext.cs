using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Minidea.Models;

namespace Minidea.DAL
{
    public class Db_MinideaContext : IdentityDbContext<AppUser>
    {
        public Db_MinideaContext()
        {

        }
        public Db_MinideaContext(DbContextOptions<Db_MinideaContext> options)
          : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=.;Database=MinideaDb;Trusted_Connection=True;MultipleActiveResultSets=true",
                    builder => builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null));
            }
            //Data Source = SQL8004.site4now.net; Initial Catalog = db_a92e67_minideadb; User Id = db_a92e67_minideadb_admin; Password = sundanceFaust123!;
            //Server=.;Database=MinideaDb;Trusted_Connection=True;MultipleActiveResultSets=true
        }

        public DbSet<BackgroundImages> BackgroundImages { get; set; } = default!;
        public DbSet<AdvertismentPhoto> AdvertismentPhotos { get; set; } = default!;
        public DbSet<AdvertismentPlace> AdvertismentPlaces { get; set; } = default!;
        public DbSet<Blogs> Blogs { get; set; } = default!;
        public DbSet<BlogsCategories> BlogsCategories { get; set; } = default!;
        public DbSet<StaticData> StaticDatas { get; set; } = default!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
