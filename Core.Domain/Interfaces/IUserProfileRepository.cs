using Core.Domain.Entities;
using System.Threading.Tasks;

namespace Core.Domain.Interfaces
{
    public interface IUserProfileRepository
    {
        Task<UserProfile> GetByIdAsync(int? id);
        Task<UserProfile> GetByEmailAsync(string email);

        Task<UserProfile> CreateAsync(UserProfile profile);
        Task<UserProfile> UpdateAsync(UserProfile profile);
        Task<UserProfile> RemoveAsync(UserProfile profile);
    }
}
