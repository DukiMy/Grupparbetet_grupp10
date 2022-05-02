using HomeFinder.Data;
using HomeFinder.Models;
using HomeFinder.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace HomeFinder.Repository
{
    public class InterestRegistrationRepository : IInterestRegistrationRepository
    {
        private readonly HomeFinderContext _context = null;
        private readonly UserManager<ApplicationUser> _userManager;
        public InterestRegistrationRepository(HomeFinderContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> AddInterestRegistration(int itemId, ApplicationUser user)
        {
            bool savedChanges = false;
            var interestRegistration = new InterestRegistration()
            {
                Item = _context.Items.FirstOrDefault(it => it.Id == itemId),
                User = await _userManager.FindByIdAsync(user.Id)
            };

            _context.InterestRegistrations.Add(interestRegistration);

            if (_context.SaveChanges() > 0)
            {
                savedChanges = true;
            }

            return savedChanges;
        }

        public IQueryable<InterestRegistrationViewModel> GetInterestRegistrationsForItemAsViewModel(int itemId)
        {

            var interestRegistrations = _context.InterestRegistrations
                .Where(i => i.Item.Id == itemId)
                .Select(i => new InterestRegistrationViewModel() { UserEmail = i.User.Email });

            return interestRegistrations;
        }
    }
}
