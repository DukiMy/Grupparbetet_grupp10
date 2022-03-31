using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HomeFinder.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Item> OwnedItems { get; set; }
        public ICollection<InterestRegistration> InterestRegistrations { get; set; }
        public string PortraitURL { get; set; }

    }
}
