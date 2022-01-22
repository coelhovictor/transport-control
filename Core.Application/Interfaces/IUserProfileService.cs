using Core.Application.DTOs;
using System.Threading.Tasks;

namespace Core.Application.Interfaces
{
    public interface IUserProfileService
    {
        Task<UserProfileDTOGet> GetByIdAsync(int? id);
        Task<UserProfileDTOGet> GetByEmailAsync(string email);
        Task AddAsync(UserProfileDTOSet profileDTO);
        Task UpdateAsync(UserProfileDTOSet profileDTO);
        Task RemoveAsync(int? id);
    }
}
