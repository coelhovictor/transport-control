using Core.Application.DTOs;
using System.Threading.Tasks;

namespace Core.Application.Interfaces
{
    public interface ITransportTagService
    {
        Task<TransportDTOGet> GetByIdAsync(string id);
        Task<TransportTagDTOGet> AddTagAsync(TransportDTOGet transportDTO, TagDTOGet tag, UserProfileDTOGet profileDTO);
        Task RemoveTagAsync(string id);
    }
}
