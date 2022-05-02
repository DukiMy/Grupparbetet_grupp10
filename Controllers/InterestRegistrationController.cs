using HomeFinder.Models;
using HomeFinder.Repository;
using HomeFinder.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HomeFinder.Controllers
{
    public class InterestRegistrationController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IInterestRegistrationRepository _interestRegistrationRepository = null;
        private readonly IItemRepository _itemRepository = null;

        public InterestRegistrationController(UserManager<ApplicationUser> userManager, IItemRepository itemRepository, IInterestRegistrationRepository interestRegistrationRepository)
        {
            _userManager = userManager;
            _interestRegistrationRepository = interestRegistrationRepository;
            _itemRepository = itemRepository;
        }

        public async Task<IActionResult> InterestRegistration(int id)
        {
            bool isSuccess = false;
            string message = string.Empty;
            bool hasRegisteredInterest = false;
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                isSuccess = true;
                hasRegisteredInterest = await _interestRegistrationRepository.GetInterestRegistrationsForItemAsViewModel(id).AnyAsync(i => i.UserEmail == user.Email);

                if (!hasRegisteredInterest)
                {
                    message = "Vill du anmäla intresse för det här objektet? Vi kommer att lämna ut ditt namn och din e-postadress till mäklaren.";
                }
                else
                {
                    message = "Du har redan anmält intresse för det här objektet.";
                }
            }
            else
            {
                message = "Du måste vara inloggad för att kunna anmäla intresse.";
            }

            var result = new InterestRegistrationJsonModel(hasRegisteredInterest, message, isSuccess);

            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddInterestRegistration(int id)
        {
            bool isSuccess = false;
            string message = string.Empty;
            bool hasRegisteredInterest = false;

            if (_itemRepository.ItemExists(id))
            {
                var user = await _userManager.GetUserAsync(User);
                hasRegisteredInterest = await _interestRegistrationRepository.GetInterestRegistrationsForItemAsViewModel(id).AnyAsync(i => i.UserEmail == user.Email);

                if (user != null && !hasRegisteredInterest)
                {
                    isSuccess = await _interestRegistrationRepository.AddInterestRegistration(id, user);
                    message = "Tack för din intresseanmälan.";
                }
            }

            var result = new InterestRegistrationJsonModel(hasRegisteredInterest, message, isSuccess);

            return Json(result);
        }

        public async Task<IActionResult> GetInterestRegistrations(int id)
        {
            var itemModel = await _itemRepository.GetItemById(id);
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                var userEmail = await _userManager.GetEmailAsync(user);

                //if (userEmail == itemModel.BrokerEmail)
                //{
                InterestRegistrationsListViewModel interestRegistrations = new()
                {
                    InterestRegistrations = await _interestRegistrationRepository.GetInterestRegistrationsForItemAsViewModel(id).ToListAsync()
                };

                return View(interestRegistrations);
                //return Json(interestRegistrations);
                //}
            }

            return NotFound();
        }
    }

    public class InterestRegistrationJsonModel
    {
        public bool IsSuccess { get; set; }
        public bool HasRegisteredInterest { get; set; }
        public string Message { get; set; }

        public InterestRegistrationJsonModel(bool hasRegisteredInterest, string message, bool isSuccess)
        {
            IsSuccess = isSuccess;
            HasRegisteredInterest = hasRegisteredInterest;
            Message = message;
        }
    }
}