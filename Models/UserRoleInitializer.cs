//using Microsoft.AspNetCore.Identity;
//using Microsoft.Extensions.DependencyInjection;
//using System;
//using System.Threading.Tasks;
//using HomeFinder.Models;

//namespace HomeFinder.Models
//{
//    public static class UserRoleInitializer
//    {
//        public static async Task InitializeAsync(IServiceProvider serviceProvider)
//        {
//            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
//            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

//            string[] roleNames = { "Admin", "User" };
//            IdentityResult roleResult;

//            foreach (var role in roleNames)
//            {
//                var roleExists = await roleManager.RoleExistsAsync(role);

//                if(!roleExists)
//                {
//                    roleResult = await roleManager.CreateAsync(new IdentityRole(role));
//                }
//            }

//            var email = "admin@site.com";
//            var password = "Qwert123!";

//            if(userManager.FindByEmailAsync(email).Result == null)
//            {
//                ApplicationUser user = new()
//                {
//                    Email = email,
//                    UserName = email,
//                    FirstName = "Admin",
//                    LastName = "Adminsson",
//                    Address = "Adstreet 3",
//                    City = "Very city",
//                    ZipCode = "12345",
//                    PhoneNumber = "0707777777",
//                };
//                IdentityResult result = userManager.CreateAsync(user, password).Result;

//                if (result.Succeeded)
//                {
//                    userManager.AddToRoleAsync(user, "Admin").Wait();
//                }
//            }

            
//        }
//    }
//}
