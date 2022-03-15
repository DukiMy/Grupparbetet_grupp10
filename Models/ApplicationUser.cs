using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace HomeFinder.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Item> OwnedItems { get; set; }
    }
}
