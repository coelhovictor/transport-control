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
    public class TransportTypeRepository : ITransportTypeRepository
    {
        ApplicationDbContext _typeContext;

        public TransportTypeRepository(ApplicationDbContext typeContext)
        {
            _typeContext = typeContext;
        }

        public async Task<TransportType> CreateAsync(TransportType type)
        {
            _typeContext.Add(type);
            await _typeContext.SaveChangesAsync();
            return type;
        }

        public async Task<TransportType> GetByIdAsync(string id, string profileId)
        {
            return await _typeContext.TransportTypes.Where(x => x.Id == id && x.UserProfileId == profileId).SingleOrDefaultAsync();
        }

        public async Task<TransportType> GetByNameAsync(string name, string profileId)
        {
            return await _typeContext.TransportTypes.Where(x => x.Name == name && x.UserProfileId == profileId).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<TransportType>> GetTransportTypesAsync(string profileId)
        {
            return await _typeContext.TransportTypes.Where(x => x.UserProfileId == profileId).ToListAsync();
        }

        public async Task<TransportType> RemoveAsync(TransportType type)
        {
            _typeContext.Remove(type);
            await _typeContext.SaveChangesAsync();
            return type;
        }

        public async Task<TransportType> UpdateAsync(TransportType type)
        {
            type.UpdatedAt = DateTime.Now;

            TransportType old = new TransportType { Id = type.Id };
            _typeContext.TransportTypes.Attach(old);
            _typeContext.Entry(old).CurrentValues.SetValues(type);

            await _typeContext.SaveChangesAsync();
            return type;
        }
    }
}
