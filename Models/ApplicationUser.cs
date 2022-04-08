using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeFinder.Models
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string FirstName { get; set; }

        [PersonalData]
        public string LastName { get; set; }

        [PersonalData]
        public string CompanyName { get; set; }

        [PersonalData]
        public string Address { get; set; }

        [PersonalData]
        public string ZipCode { get; set; }

        [PersonalData]
        public string City { get; set; }

        [PersonalData]
        [DataType(DataType.Date)]
        public DateTime UserCreationDate { get; set; } = DateTime.Now;

        public string PortraitURL { get; set; }

        public ICollection<Item> OwnedItems { get; set; }
        public ICollection<InterestRegistration> InterestRegistrations { get; set; }
    }
}
