using AutoMapper;
using Core.Application.DTOs;
using Core.Application.Interfaces;
using Core.Domain.Entities;
using Core.Domain.Interfaces;
using System.Threading.Tasks;

namespace Core.Application.Services
{
    public class UserProfileService : IUserProfileService
    {
        private IUserProfileRepository _profileRepository;
        private readonly IMapper _mapper;

        public UserProfileService(IUserProfileRepository profileRepository, IMapper mapper)
        {
            _profileRepository = profileRepository;
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
    }
}
