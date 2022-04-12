using Microsoft.EntityFrameworkCore;
using HomeFinder.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace HomeFinder.Data
{
    public class HomeFinderContext : IdentityDbContext<ApplicationUser>
    {
        public HomeFinderContext(DbContextOptions<HomeFinderContext> options)
            : base(options)
        {

        }
        public DbSet<Item> Items { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ItemType> ItemTypes { get; set; }
        public DbSet<Recommendation> Recommendations { get; set; }
        public DbSet<InterestRegistration> InterestRegistrations { get; set; }

    }
}
