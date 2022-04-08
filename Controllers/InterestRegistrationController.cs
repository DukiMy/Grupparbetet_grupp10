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
        private readonly IItemRepository _itemRepository = null;

        public InterestRegistrationController(UserManager<ApplicationUser> userManager, IItemRepository itemRepository)
        {
            _userManager = userManager;
            _itemRepository = itemRepository;
        }

        //public async Task<IActionResult> RegisterInterest(int id)
        //{

        //    return View();
        //}

        [HttpPost]
        public async Task<IActionResult> RegisterInterest(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                //var userEmail = await _userManager.GetEmailAsync(user);
                bool hasRegisteredInterest = await _itemRepository.GetInterestRegistrationsAsViewModel(id).AnyAsync(i => i.UserEmail == user.Email);

                if(!hasRegisteredInterest)
                {
                    var model = new InterestRegistrationViewModel()
                    {
                        UserId = user.Id,
                        ItemId = id
                    };

                    await _itemRepository.AddInterestRegistrationFromModel(model);
                }
            }

            return RedirectToRoute("itemDetailsRoute", new { id });
        }


        public async Task<IActionResult> GetInterestRegistrations(int id)
        {
            var itemModel = await _itemRepository.GetItemById(id);
            var user = await _userManager.GetUserAsync(User);

            if(user != null)
            {
                var userEmail = await _userManager.GetEmailAsync(user);

                //if (userEmail == itemModel.BrokerEmail)
                //{
                    InterestRegistrationsListViewModel interestRegistrations = new() 
                    { 
                        InterestRegistrations = await _itemRepository.GetInterestRegistrationsAsViewModel(id).ToListAsync() 
                    };

                    return View(interestRegistrations);
                //}
            }

            return NotFound();
        }
    }
}
