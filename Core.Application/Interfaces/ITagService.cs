using Core.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Application.Interfaces
{
    public interface ITagService
    {
        Task<IEnumerable<TagDTOGet>> GetTagsAsync(UserProfileDTOGet profileDTO);
        Task<TagDTOGet> GetByIdAsync(string id, UserProfileDTOGet profileDTO);
        Task<TagDTOGet> GetByNameAsync(string name, UserProfileDTOGet profileDTO);
        Task AddAsync(TagDTOSet tagDTO, UserProfileDTOGet profileDTO);
        Task UpdateAsync(TagDTOSet tagDTO, UserProfileDTOGet profileDTO);
        Task RemoveAsync(string id, UserProfileDTOGet profileDTO);
    }
}
