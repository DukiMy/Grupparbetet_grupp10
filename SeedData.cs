using HomeFinder.Models;
using Microsoft.AspNetCore.Identity;

namespace HomeFinder
{
    public static class SeedData
    {
        public static void Seed(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            SeedRole(roleManager, "Admin");
            SeedRole(roleManager, "Broker");
            SeedRole(roleManager, "Customer");

            SeedUser(userManager, "Admin@house.se", "Abc123!!!", "Admin");
            SeedUser(userManager, "Broker@house.se", "Abc123!!!", "Broker");
            SeedUser(userManager, "Customer@house.se", "Abc123!!!", "Customer");
        }

        private static void SeedUser(UserManager<ApplicationUser> userManager, string email, string password, string roleName)
        {
            if (userManager.FindByNameAsync(roleName).Result == null)
            {
                var user = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true,
                };
                var result = userManager.CreateAsync(user, password).Result;
                if(result.Succeeded)
                {
                    userManager.AddToRoleAsync((ApplicationUser)user, roleName).Wait();
                }
            }
        }

        private static void SeedRole(RoleManager<IdentityRole> roleManager, string roleName)
        {
            if(!roleManager.RoleExistsAsync(roleName).Result)
            {
                var role = new IdentityRole
                {
                    Name = roleName
                };
                var result = roleManager.CreateAsync(role).Result;
            }
        }
    }
}
