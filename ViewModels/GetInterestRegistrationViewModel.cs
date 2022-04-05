using System.ComponentModel.DataAnnotations;

namespace HomeFinder.ViewModels
{
    public class GetInterestRegistrationViewModel
    {
        [Required]
        public string UserEmail { get; set; }
    }
}
