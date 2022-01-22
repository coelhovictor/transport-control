using AutoMapper;
using Core.Application.DTOs;
using Core.Application.Interfaces;
using Core.Domain.Account;
using Core.Domain.Entities;
using Core.Domain.Interfaces;
using Core.Domain.Models;
using System.Threading.Tasks;

namespace Core.Application.Services
{
    public class UserProfileService : IUserProfileService
    {
        private IUserProfileRepository _profileRepository;
        private readonly IAuthenticate _authentication;
        private readonly IMapper _mapper;

        public UserProfileService(IUserProfileRepository profileRepository, IAuthenticate authentication,
            IMapper mapper)
        {
            _profileRepository = profileRepository;
            _authentication = authentication;
            _mapper = mapper;
        }

        public async Task AddAsync(UserProfileDTOSet profileDTO)
        {
            var profileEntity = _mapper.Map<UserProfile>(profileDTO);
            await _profileRepository.CreateAsync(profileEntity);
        }

        public async Task<UserProfileDTOGet> GetByEmailAsync(string email)
        {
            var profileEntity = await _profileRepository.GetByEmailAsync(email);
            return _mapper.Map<UserProfileDTOGet>(profileEntity);
        }

        public async Task<UserProfileDTOGet> GetByIdAsync(int? id)
        {
            var profileEntity = await _profileRepository.GetByIdAsync(id);
            return _mapper.Map<UserProfileDTOGet>(profileEntity);
        }

        public async Task RemoveAsync(int? id)
        {
            var profileEntity = _profileRepository.GetByIdAsync(id).Result;
            await _profileRepository.RemoveAsync(profileEntity);
        }

        public async Task UpdateAsync(UserProfileDTOSet profileDTO)
        {
            var profileEntity = _mapper.Map<UserProfile>(profileDTO);
            await _profileRepository.UpdateAsync(profileEntity);
        }

        public async Task<ChangePasswordAttempt> ChangePassword(string email, string password, 
            string newPassword, string confirmPassword)
        {
            if (string.IsNullOrEmpty(password))
                return new ChangePasswordAttempt() { Success = false, Message = "Current Password is required." };

            if (string.IsNullOrEmpty(newPassword))
                return new ChangePasswordAttempt() { Success = false, Message = "Password is required." };

            if (string.IsNullOrEmpty(confirmPassword))
                return new ChangePasswordAttempt() { Success = false, Message = "Confirm Password is required." };

            if (newPassword != confirmPassword)
                return new ChangePasswordAttempt() { Success = false, Message = "Passwords don`t match." };

            return await _authentication.ChangePassword(email, password, newPassword);
        }
    }
}
