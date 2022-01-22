using AutoMapper;
using Core.Application.DTOs;
using Core.Application.Interfaces;
using Core.Domain.Entities;
using Core.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Application.Services
{
    public class TransportTypeService : ITransportTypeService
    {
        private ITransportTypeRepository _typeRepository;
        private readonly IMapper _mapper;

        public TransportTypeService(ITransportTypeRepository typeRepository, IMapper mapper)
        {
            _typeRepository = typeRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(TransportTypeDTOSet typeDTO, UserProfileDTOGet profileDTO)
        {
            var typeEntity = _mapper.Map<TransportType>(typeDTO);
            typeEntity.UserProfileId = profileDTO.Id;
            await _typeRepository.CreateAsync(typeEntity);
        }

        public async Task<TransportTypeDTOGet> GetByIdAsync(string id, UserProfileDTOGet profileDTO)
        {
            var typeDTO = await _typeRepository.GetByIdAsync(id, profileDTO.Id);
            return _mapper.Map<TransportTypeDTOGet>(typeDTO);
        }

        public async Task<TransportTypeDTOGet> GetByNameAsync(string name, UserProfileDTOGet profileDTO)
        {
            var typeDTO = await _typeRepository.GetByNameAsync(name, profileDTO.Id);
            return _mapper.Map<TransportTypeDTOGet>(typeDTO);
        }

        public async Task<IEnumerable<TransportTypeDTOGet>> GetTransportTypesAsync(UserProfileDTOGet profileDTO)
        {
            var typesEntity = await _typeRepository.GetTransportTypesAsync(profileDTO.Id);
            return _mapper.Map<IEnumerable<TransportTypeDTOGet>>(typesEntity);
        }

        public async Task RemoveAsync(string id, UserProfileDTOGet profileDTO)
        {
            var typeEntity = _typeRepository.GetByIdAsync(id, profileDTO.Id).Result;
            await _typeRepository.RemoveAsync(typeEntity);
        }

        public async Task UpdateAsync(TransportTypeDTOSet typeDTO, UserProfileDTOGet profileDTO)
        {
            var typeEntity = _mapper.Map<TransportType>(typeDTO);
            typeEntity.UserProfileId = profileDTO.Id;

            await _typeRepository.UpdateAsync(typeEntity);
        }
    }
}
