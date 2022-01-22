using AutoMapper;
using Core.Application.DTOs;
using Core.Application.Interfaces;
using Core.Domain.Entities;
using Core.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Application.Services
{
    public class ReasonService : IReasonService
    {
        private IReasonRepository _reasonRepository;
        private readonly IMapper _mapper;

        public ReasonService(IReasonRepository reasonRepository, IMapper mapper)
        {
            _reasonRepository = reasonRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(ReasonDTOSet reasonDTO, UserProfileDTOGet profileDTO)
        {
            var reasonEntity = _mapper.Map<Reason>(reasonDTO);
            reasonEntity.UserProfileId = profileDTO.Id;
            await _reasonRepository.CreateAsync(reasonEntity);
        }

        public async Task<ReasonDTOGet> GetByIdAsync(string id, UserProfileDTOGet profileDTO)
        {
            var reasonDTO = await _reasonRepository.GetByIdAsync(id, profileDTO.Id);
            return _mapper.Map<ReasonDTOGet>(reasonDTO);
        }

        public async Task<ReasonDTOGet> GetByNameAsync(string name, UserProfileDTOGet profileDTO)
        {
            var reasonDTO = await _reasonRepository.GetByNameAsync(name, profileDTO.Id);
            return _mapper.Map<ReasonDTOGet>(reasonDTO);
        }

        public async Task<IEnumerable<ReasonDTOGet>> GetReasonsAsync(UserProfileDTOGet profileDTO)
        {
            var reasonEntity = await _reasonRepository.GetReasonsAsync(profileDTO.Id);
            return _mapper.Map<IEnumerable<ReasonDTOGet>>(reasonEntity);
        }

        public async Task RemoveAsync(string id, UserProfileDTOGet profileDTO)
        {
            var reasonEntity = _reasonRepository.GetByIdAsync(id, profileDTO.Id).Result;
            await _reasonRepository.RemoveAsync(reasonEntity);
        }

        public async Task UpdateAsync(ReasonDTOSet reasonDTO, UserProfileDTOGet profileDTO)
        {
            var reasonEntity = _mapper.Map<Reason>(reasonDTO);
            reasonEntity.UserProfileId = profileDTO.Id;

            await _reasonRepository.UpdateAsync(reasonEntity);
        }
    }
}
