using HomeFinder.Models;
using HomeFinder.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace HomeFinder.Repository
{
    public interface IInterestRegistrationRepository
    {
        public Task<bool> AddInterestRegistration(int itemId, ApplicationUser user);

        public IQueryable<InterestRegistrationViewModel> GetInterestRegistrationsForItemAsViewModel(int itemId);
    }
}
