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
        public DbSet<Item> Item { get; set; }
        public DbSet<Image> Image { get; set; }

    }
}
