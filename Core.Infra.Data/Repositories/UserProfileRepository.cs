using Core.Domain.Entities;
using Core.Domain.Interfaces;
using Core.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Infra.Data.Repositories
{
    public class UserProfileRepository : IUserProfileRepository
    {
        ApplicationDbContext _profileContext;

        public UserProfileRepository(ApplicationDbContext profileContext)
        {
            _profileContext = profileContext;
        }

        public async Task<UserProfile> CreateAsync(UserProfile profile)
        {
            _profileContext.Add(profile);
            await _profileContext.SaveChangesAsync();
            return profile;
        }

        public async Task<UserProfile> GetByEmailAsync(string email)
        {
            return await _profileContext.UserProfiles.SingleOrDefaultAsync(x => x.Email == email);
        }

        public async Task<UserProfile> GetByIdAsync(int? id)
        {
            return await _profileContext.UserProfiles.FindAsync(id);
        }

        public async Task<UserProfile> RemoveAsync(UserProfile profile)
        {
            _profileContext.Remove(profile);
            await _profileContext.SaveChangesAsync();
            return profile;
        }

        public async Task<UserProfile> UpdateAsync(UserProfile profile)
        {
            if (!await _profileContext.UserProfiles.Where(x => x.Id == profile.Id
                 && x.Email == profile.Email).AnyAsync()) return null;

            profile.UpdatedAt = DateTime.Now;

            UserProfile old = new UserProfile { Id = profile.Id };
            _profileContext.UserProfiles.Attach(old);
            _profileContext.Entry(old).CurrentValues.SetValues(profile);

            await _profileContext.SaveChangesAsync();
            return profile;
        }
    }
}
