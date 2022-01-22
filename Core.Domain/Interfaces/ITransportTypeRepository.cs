using Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Domain.Interfaces
{
    public interface ITransportTypeRepository
    {
        Task<IEnumerable<TransportType>> GetTransportTypesAsync(string profileId);
        Task<TransportType> GetByIdAsync(string id, string profileId);
        Task<TransportType> GetByNameAsync(string name, string profileId);

        Task<TransportType> CreateAsync(TransportType type);
        Task<TransportType> UpdateAsync(TransportType type);
        Task<TransportType> RemoveAsync(TransportType type);
    }
}
