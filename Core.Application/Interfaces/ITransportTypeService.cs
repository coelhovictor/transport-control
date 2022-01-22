using Core.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Application.Interfaces
{
    public interface ITransportTypeService
    {
        Task<IEnumerable<TransportTypeDTOGet>> GetTransportTypesAsync(UserProfileDTOGet profileDTO);
        Task<TransportTypeDTOGet> GetByIdAsync(string id, UserProfileDTOGet profileDTO);
        Task<TransportTypeDTOGet> GetByNameAsync(string name, UserProfileDTOGet profileDTO);
        Task AddAsync(TransportTypeDTOSet typeDTO, UserProfileDTOGet profileDTO);
        Task UpdateAsync(TransportTypeDTOSet typeDTO, UserProfileDTOGet profileDTO);
        Task RemoveAsync(string id, UserProfileDTOGet profileDTO);
    }
}
