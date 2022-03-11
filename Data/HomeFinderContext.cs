using Microsoft.EntityFrameworkCore;
using HomeFinder.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace HomeFinder.Data
{
    public class HomeFinderContext : IdentityDbContext
    {
        public HomeFinderContext(DbContextOptions<HomeFinderContext> options)
            : base(options)
        {

        }
        public DbSet<House> House { get; set; }
    }
}
