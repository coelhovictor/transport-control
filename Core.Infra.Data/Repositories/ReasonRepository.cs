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
    public class ReasonRepository : IReasonRepository
    {
        ApplicationDbContext _reasonContext;

        public ReasonRepository(ApplicationDbContext reasonContext)
        {
            _reasonContext = reasonContext;
        }

        public async Task<Reason> CreateAsync(Reason reason)
        {
            _reasonContext.Add(reason);
            await _reasonContext.SaveChangesAsync();
            return reason;
        }

        public async Task<Reason> GetByIdAsync(string id, string profileId)
        {
            return await _reasonContext.Reasons.Where(x => x.Id == id && x.UserProfileId == profileId).SingleOrDefaultAsync();
        }

        public async Task<Reason> GetByNameAsync(string name, string profileId)
        {
            return await _reasonContext.Reasons.Where(x => x.Name == name && x.UserProfileId == profileId).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Reason>> GetReasonsAsync(string profileId)
        {
            return await _reasonContext.Reasons.Where(x => x.UserProfileId == profileId).ToListAsync();
        }

        public async Task<Reason> RemoveAsync(Reason reason)
        {
            _reasonContext.Remove(reason);
            await _reasonContext.SaveChangesAsync();
            return reason;
        }

        public async Task<Reason> UpdateAsync(Reason reason)
        {
            reason.UpdatedAt = DateTime.Now;

            _reasonContext.Entry(reason).State = EntityState.Modified;
            _reasonContext.Entry(reason).Property(p => p.Id).IsModified = false;
            _reasonContext.Entry(reason).Property(p => p.CreatedAt).IsModified = false;

            await _reasonContext.SaveChangesAsync();
            return reason;
        }
    }
}
