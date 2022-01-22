using Core.Domain.Entities;
using Core.Domain.Interfaces;
using Core.Infra.Data.Context;
using System.Threading.Tasks;

namespace Core.Infra.Data.Repositories
{
    public class TransportTagRepository : ITransportTagRepository
    {
        ApplicationDbContext _typeContext;

        public TransportTagRepository(ApplicationDbContext typeContext)
        {
            _typeContext = typeContext;
        }

        public async Task<TransportTag> GetByIdAsync(string id)
        {
            return await _typeContext.TransportTags.FindAsync(id);
        }

        public async Task<TransportTag> AddTagAsync(Transport transport, Tag tag)
        {
            TransportTag transportTag = new TransportTag() { TransportId = transport.Id, TagId = tag.Id };
            _typeContext.Add(transportTag);
            await _typeContext.SaveChangesAsync();
            return transportTag;
        }

        public async Task<TransportTag> RemoveTagAsync(TransportTag transportTag)
        {
            _typeContext.Remove(transportTag);
            await _typeContext.SaveChangesAsync();
            return transportTag;
        }
    }
}
