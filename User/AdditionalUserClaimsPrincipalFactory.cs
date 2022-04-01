using HomeFinder.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HomeFinder.User
{
    public class AdditionalUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
    {
        public AdditionalUserClaimsPrincipalFactory(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, roleManager, optionsAccessor)
        { 

        }

        public async override Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
        {
            var claimsPrincipal = await base.CreateAsync(user);
            var identity = (ClaimsIdentity)claimsPrincipal.Identity;

            if (!string.IsNullOrWhiteSpace(user.FirstName))
            {
                identity.AddClaims(new[] {
            new Claim(ClaimTypes.GivenName, user.FirstName)
          });
            }

            if (!string.IsNullOrWhiteSpace(user.LastName))
            {
                identity.AddClaims(new[] {
            new Claim(ClaimTypes.Surname, user.LastName),
          });
            }
            return claimsPrincipal;
        }
    }
}
