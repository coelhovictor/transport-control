using Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Domain.Interfaces
{
    public interface IReasonRepository
    {
        Task<IEnumerable<Reason>> GetReasonsAsync(string profileId);
        Task<Reason> GetByIdAsync(string? id, string profileId);
        Task<Reason> GetByNameAsync(string name, string profileId);

        Task<Reason> CreateAsync(Reason reason);
        Task<Reason> UpdateAsync(Reason reason);
        Task<Reason> RemoveAsync(Reason reason);
    }
}
