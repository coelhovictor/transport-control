using Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Domain.Interfaces
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>> GetTagsAsync(string profileId);
        Task<Tag> GetByIdAsync(string id, string profileId);
        Task<Tag> GetByNameAsync(string name, string profileId);

        Task<Tag> CreateAsync(Tag tag);
        Task<Tag> UpdateAsync(Tag tag);
        Task<Tag> RemoveAsync(Tag tag);
    }
}
