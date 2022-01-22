using AutoMapper;
using Core.Application.DTOs;
using Core.Application.Interfaces;
using Core.Domain.Entities;
using Core.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Application.Services
{
    public class TagService : ITagService
    {
        private ITagRepository _tagRepository;
        private readonly IMapper _mapper;

        public TagService(ITagRepository tagRepository, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(TagDTOSet tagDTO, UserProfileDTOGet profileDTO)
        {
            var tagEntity = _mapper.Map<Tag>(tagDTO);
            tagEntity.UserProfileId = profileDTO.Id;
            await _tagRepository.CreateAsync(tagEntity);
        }

        public async Task<TagDTOGet> GetByIdAsync(string id, UserProfileDTOGet profileDTO)
        {
            var tagDTO = await _tagRepository.GetByIdAsync(id, profileDTO.Id);
            return _mapper.Map<TagDTOGet>(tagDTO);
        }

        public async Task<TagDTOGet> GetByNameAsync(string name, UserProfileDTOGet profileDTO)
        {
            var tagDTO = await _tagRepository.GetByNameAsync(name, profileDTO.Id);
            return _mapper.Map<TagDTOGet>(tagDTO);
        }

        public async Task<IEnumerable<TagDTOGet>> GetTagsAsync(UserProfileDTOGet profileDTO)
        {
            var tagEntity = await _tagRepository.GetTagsAsync(profileDTO.Id);
            return _mapper.Map<IEnumerable<TagDTOGet>>(tagEntity);
        }

        public async Task RemoveAsync(string id, UserProfileDTOGet profileDTO)
        {
            var tagEntity = _tagRepository.GetByIdAsync(id, profileDTO.Id).Result;
            await _tagRepository.RemoveAsync(tagEntity);
        }

        public async Task UpdateAsync(TagDTOSet tagDTO, UserProfileDTOGet profileDTO)
        {
            var tagEntity = _mapper.Map<Tag>(tagDTO);
            tagEntity.UserProfileId = profileDTO.Id;

            await _tagRepository.UpdateAsync(tagEntity);
        }
    }
}
