using Core.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Application.Interfaces
{
    public interface IReasonService
    {
        Task<IEnumerable<ReasonDTOGet>> GetReasonsAsync(UserProfileDTOGet profileDTO);
        Task<ReasonDTOGet> GetByIdAsync(string id, UserProfileDTOGet profileDTO);
        Task<ReasonDTOGet> GetByNameAsync(string name, UserProfileDTOGet profileDTO);
        Task AddAsync(ReasonDTOSet reasonDTO, UserProfileDTOGet profileDTO);
        Task UpdateAsync(ReasonDTOSet reasonDTO, UserProfileDTOGet profileDTO);
        Task RemoveAsync(string id, UserProfileDTOGet profileDTO);
    }
}
