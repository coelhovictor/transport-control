using Core.Domain.Entities;
using System.Threading.Tasks;

namespace Core.Domain.Interfaces
{
    public interface ITransportTagRepository
    {
        Task<TransportTag> GetByIdAsync(string id);
        Task<TransportTag> AddTagAsync(Transport transport, Tag tag);
        Task<TransportTag> RemoveTagAsync(TransportTag transportTag);
    }
}
