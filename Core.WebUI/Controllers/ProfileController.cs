using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Core.Application.DTOs;
using Core.Application.Interfaces;
using Core.Infra.Data.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Core.WebUI.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IUserProfileService _profileService;
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileController(IUserProfileService profileService,
            IConfiguration configuration, UserManager<ApplicationUser> userManager)
        {
            _profileService = profileService;
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var profile = await _profileService.GetByEmailAsync(User.Identity.Name);
            if (profile == null) return Unauthorized("Account not found");

            return View(profile);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, UserProfileDTOSet profile, List<IFormFile> files)
        {
            if (!ModelState.IsValid)
                return BadRequest(Utilities.InvalidStateMessages(ModelState));

            if (profile.Email != User.Identity.Name) return Unauthorized("Account not found");

            if (profile == null) return BadRequest("Invalid data");
            profile.Id = id;

            //CHANGE PICTURE PROFILE
            string picture = "";
            if(files.Any())
            {
                string[] extensions = new string[3] { ".jpg", ".jpeg", ".png" };

                var file = files.First();
                if (!extensions.Contains(Path.GetExtension(file.FileName))) 
                    return BadRequest("Invalid picture file type");
                
                picture = await UploadImage(file);
                if (string.IsNullOrEmpty(picture))
                    return BadRequest("An error occurred");
            }

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            user.FirstName = profile.FirstName;
            user.LastName = profile.LastName;
            if (!string.IsNullOrEmpty(picture)) user.Picture = picture;
            await _userManager.UpdateAsync(user);

            //CHANGE PASSWORD
            if (!string.IsNullOrEmpty(profile.Password))
            {
                var result = await _profileService.ChangePassword(User.Identity.Name, profile.CurrentPassword, profile.Password,
                    profile.ConfirmPassword);

                if (!result.Success)
                    return BadRequest(result.Message);
            }

            await _profileService.UpdateAsync(profile);
            return Json(profile);
        }

        private async Task<string> UploadImage(IFormFile file)
        {
            Account account;

            string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (env == "Development")
            {
                account = new Account(
                    _configuration.GetSection("AppSettings").GetSection("CloudinarySettings")["CloudName"],
                    _configuration.GetSection("AppSettings").GetSection("CloudinarySettings")["ApiKey"],
                    _configuration.GetSection("AppSettings").GetSection("CloudinarySettings")["ApiSecret"]
                );
            } else
            {
                account = new Account(
                    Environment.GetEnvironmentVariable("CLOUDINARY_CLOUD_NAME"),
                    Environment.GetEnvironmentVariable("CLOUDINARY_API_KEY"),
                    Environment.GetEnvironmentVariable("CLOUDINARY_API_SECRET")
                );
            }

            Cloudinary cloudinary = new Cloudinary(account);

            List<string> ls = new List<string>() { ".png", ".jpg", ".jpeg" };

            string extension = Path.GetExtension(file.FileName).ToLower();
            if (!ls.Contains(extension)) return "";
            if (file.Length > 1048576) return "";

            string fileName = $"{Guid.NewGuid().ToString()}{extension}";

            ImageUploadParams uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(fileName, file.OpenReadStream()),
                Folder = "tcontrol-pictures"
            };

            ImageUploadResult result = await cloudinary.UploadAsync(uploadParams);

            if (result == null || result.Url == null) return "";

            return result.Url.ToString();
        }
    }
}
