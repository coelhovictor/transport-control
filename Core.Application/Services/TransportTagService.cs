using AutoMapper;
using Core.Application.DTOs;
using Core.Application.Interfaces;
using Core.Domain.Entities;
using Core.Domain.Interfaces;
using System.Threading.Tasks;

namespace Core.Application.Services
{
    public class TransportTagService : ITransportTagService
    {
        private ITransportTagRepository _transportTagRepository;
        private readonly IMapper _mapper;

        public TransportTagService(ITransportTagRepository transportTagRepository, IMapper mapper)
        {
            _transportTagRepository = transportTagRepository;
            _mapper = mapper;
        }

        public async Task<TransportDTOGet> GetByIdAsync(string id)
        {
            var transportDTO = await _transportTagRepository.GetByIdAsync(id);
            return _mapper.Map<TransportDTOGet>(transportDTO);
        }
        public async Task<TransportTagDTOGet> AddTagAsync(TransportDTOGet transportDTO, TagDTOGet tag, UserProfileDTOGet profileDTO)
        {
            var transportEntity = _mapper.Map<Transport>(transportDTO);
            var tagEntity = _mapper.Map<Tag>(tag);

            var transportTagEntity = await _transportTagRepository.AddTagAsync(transportEntity, tagEntity);
            return _mapper.Map<TransportTagDTOGet>(transportTagEntity);
        }

        public async Task RemoveTagAsync(string id)
        {
            var transportTagEntity = _transportTagRepository.GetByIdAsync(id).Result;
            await _transportTagRepository.RemoveTagAsync(transportTagEntity);
        }
    }
}
