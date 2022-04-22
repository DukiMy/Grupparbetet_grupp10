﻿using HomeFinder.Models;
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

        //public async Task<IActionResult> InterestRegistration(int id, string message, bool isSuccess = false)
        //{
        //    InterestRegistrationViewModel model = new();

        //    var user = await _userManager.GetUserAsync(User);

        //    if (!isSuccess)
        //    {
        //        bool hasRegisteredInterest = await _itemRepository.GetInterestRegistrationsAsViewModel(id).AnyAsync(i => i.UserEmail == user.Email);

        //        if (user != null && !hasRegisteredInterest)
        //        {
        //            model.UserId = user.Id;
        //            model.ItemId = id;
        //        }
        //        else if (hasRegisteredInterest)
        //        {
        //            message = "Du har redan anmält intresse för det här objektet.";
        //            return RedirectToAction(nameof(InterestRegistration), new { id = id, message = message, isSuccess = true });
        //        }
        //    }

        //    ViewBag.IsSuccess = isSuccess;
        //    ViewBag.Message = message;
        //    ViewBag.ItemId = id;
        //    return View(model);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> InterestRegistration(InterestRegistrationViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await _itemRepository.AddInterestRegistrationFromModel(model);

        //        string message = "Tack för visat intresse.";

        //        return RedirectToAction(nameof(InterestRegistration), new { id = model.ItemId, message = message, isSuccess = true });
        //    }

        //    return View();
        //}

        public async Task<IActionResult> InterestRegistrationAjax(int id)
        {
            string message = string.Empty;
            
            var user = await _userManager.GetUserAsync(User);
            bool hasRegisteredInterest = await _itemRepository.GetInterestRegistrationsAsViewModel(id).AnyAsync(i => i.UserEmail == user.Email);

            if (user != null && !hasRegisteredInterest)
            {
                message = "Vill du anmäla intresse för det här objektet? Vi kommer att lämna ut ditt namn och din e-postadress till mäklaren.";
            }
            else if (hasRegisteredInterest)
            {
                message = "Du har redan anmält intresse för det här objektet.";
            }

            var result = new JsonModel(hasRegisteredInterest, message);

            return Json(result);
        }


        [HttpPost]
        public async Task<IActionResult> PostInterestRegistrationAjax(int id)
        {
            bool isSuccess = false;
            if (_itemRepository.ItemExists(id))
            {
                var user = await _userManager.GetUserAsync(User);
                bool hasRegisteredInterest = await _itemRepository.GetInterestRegistrationsAsViewModel(id).AnyAsync(i => i.UserEmail == user.Email);

                if (user != null && !hasRegisteredInterest)
                {
                    InterestRegistrationViewModel model = new()
                    {
                        UserId = user.Id,
                        ItemId = id
                    };

                    isSuccess = await _itemRepository.AddInterestRegistrationFromModel(model);
                }
            }

            return Json(isSuccess);
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
                    InterestRegistrations = await _itemRepository.GetInterestRegistrationsAsViewModel(id).ToListAsync()
                };

                return View(interestRegistrations);
                //}
            }

            return NotFound();
        }
    }

    public class JsonModel
    {
        public bool HasRegisteredInterest { get; set; }
        public string Message { get; set; }

        public JsonModel(bool hasRegisteredInterest, string message)
        {
            HasRegisteredInterest = hasRegisteredInterest;
            Message = message;
        }
    }
}