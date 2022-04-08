using System.ComponentModel.DataAnnotations;

namespace HomeFinder.ViewModels
{
    public class AddInterestRegistrationViewModel
    {
        [Required]
        public int ItemId { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
