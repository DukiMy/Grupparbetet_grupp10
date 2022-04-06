using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeFinder.ViewModels
{
    public class InterestRegistrationsViewModel
    {
        [Required]
        public List<string> UserEmails { get; set; }
    }
}
