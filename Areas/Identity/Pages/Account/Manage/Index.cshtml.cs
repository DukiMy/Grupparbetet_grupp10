using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HomeFinder.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeFinder.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _webHostEnvironment = webHostEnvironment;
        }

        public string FirstName { get; set; }
        public string PortraitURL { get; set; }
        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Display(Name = "Foto")]
            public IFormFile Portrait { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Förnamn")]
            public string FirstName { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Efternamn")]
            public string LastName { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Mäklarfirma")]
            public string CompanyName { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Adress")]
            public string Address { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Postkod")]
            public string ZipCode { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Ort")]
            public string City { get; set; }

            [Phone]
            [Display(Name = "Telefonnummer")]
            public string PhoneNumber { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            
            Username = userName;
            PortraitURL = user.PortraitURL;

            Input = new InputModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                CompanyName = user.CompanyName,
                Address = user.Address,
                ZipCode = user.ZipCode,
                City = user.City,
                PhoneNumber = phoneNumber
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }
            if (Input.Portrait != null)
            {
                string folder = "img/";
                user.PortraitURL = await UploadImage(folder, Input.Portrait);
            }
            if (Input.FirstName != user.FirstName)
            {
                user.FirstName = Input.FirstName;
            }

            if (Input.LastName != user.LastName)
            {
                user.LastName = Input.LastName;
            }
            if (Input.CompanyName != user.CompanyName)
            {
                user.CompanyName = Input.CompanyName;
            }
            if (Input.Address != user.Address)
            {
                user.Address = Input.Address;
            }
            if (Input.ZipCode != user.ZipCode)
            {
                user.ZipCode = Input.ZipCode;
            }
            if (Input.City != user.City)
            {
                user.City = Input.City;
            }
            if (Input.PhoneNumber != user.PhoneNumber)
            {
                user.PhoneNumber = Input.PhoneNumber;
            }
            await _userManager.UpdateAsync(user);

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Din profil har uppdaterats";
            return RedirectToPage();
        }
        private async Task<string> UploadImage(string folderPath, IFormFile file)
        {
            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;

            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);

            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            return "/" + folderPath;
        }
    }
}
