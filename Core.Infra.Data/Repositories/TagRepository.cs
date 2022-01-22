using Core.Domain.Entities;
using Core.Domain.Interfaces;
using Core.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Infra.Data.Repositories
{
    public class TagRepository : ITagRepository
    {
        ApplicationDbContext _tagContext;

        public TagRepository(ApplicationDbContext tagContext)
        {
            _tagContext = tagContext;
        }

        public async Task<Tag> CreateAsync(Tag tag)
        {
            _tagContext.Add(tag);
            await _tagContext.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag> GetByIdAsync(string id, string profileId)
        {
            return await _tagContext.Tags.Where(x => x.Id == id && x.UserProfileId == profileId).SingleOrDefaultAsync();
        }

        public async Task<Tag> GetByNameAsync(string name, string profileId)
        {
            return await _tagContext.Tags.Where(x => x.Name == name && x.UserProfileId == profileId).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Tag>> GetTagsAsync(string profileId)
        {
            return await _tagContext.Tags.Where(x => x.UserProfileId == profileId).ToListAsync();
        }

        public async Task<Tag> RemoveAsync(Tag tag)
        {
            _tagContext.Remove(tag);
            await _tagContext.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag> UpdateAsync(Tag tag)
        {
            tag.UpdatedAt = DateTime.Now;

            Tag old = new Tag { Id = tag.Id };
            _tagContext.Tags.Attach(old);
            _tagContext.Entry(old).CurrentValues.SetValues(tag);

            await _tagContext.SaveChangesAsync();
            return tag;
        }
    }
}
