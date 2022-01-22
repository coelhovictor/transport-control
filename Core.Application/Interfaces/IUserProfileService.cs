using Core.Application.DTOs;
using Core.Domain.Models;
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
        Task<ChangePasswordAttempt> ChangePassword(string email, string password, string newPassword, string confirmPassword);
    }
}
