using Core.Domain.Account;
using Core.Domain.Entities;
using Core.Domain.Interfaces;
using Core.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Infra.Data.Identity
{
    public class AuthenticateService : IAuthenticate
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserProfileRepository _profileRepository;

        public AuthenticateService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            IUserProfileRepository profileRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _profileRepository = profileRepository;
        }

        public async Task<bool> Authenticate(string email, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, false, lockoutOnFailure: false);
            return result.Succeeded;
        }

        public async Task<RegisterAttempt> RegisterUser(string email, string password, string firstName, string lastName, 
            DateTime? birthDate, string profession, string location)
        {
            var applicationUser = new ApplicationUser(email, email, firstName, lastName);
            var result = await _userManager.CreateAsync(applicationUser, password);

            if (result.Succeeded)
            {
                if(_profileRepository.GetByEmailAsync(applicationUser.Email).Result == null)
                {
                    await _profileRepository.CreateAsync(new UserProfile(applicationUser.Email,
                            birthDate, profession, location));
                }
                await _signInManager.SignInAsync(applicationUser, isPersistent: false);
            }

            string message = "";
            if (result.Errors.Any())
                message = result.Errors.First().Description;

            return new RegisterAttempt { Success = result.Succeeded, 
                Message = message };
        }

        public async Task<ChangePasswordAttempt> ChangePassword(string email, string currentPassword, string newPassword)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return new ChangePasswordAttempt() { Success = false, Message = "Account not found." };

            IdentityResult result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

            string message = "";
            if (result.Errors.Any())
                message = result.Errors.First().Description;

            return new ChangePasswordAttempt
            {
                Success = result.Succeeded,
                Message = message
            };
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
