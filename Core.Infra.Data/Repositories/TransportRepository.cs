using Core.Domain.Entities;
using Core.Domain.Interfaces;
using Core.Domain.Models;
using Core.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Infra.Data.Repositories
{
    public class TransportRepository : ITransportRepository
    {
        ApplicationDbContext _typeContext;

        public TransportRepository(ApplicationDbContext typeContext)
        {
            _typeContext = typeContext;
        }

        public async Task<Transport> CreateAsync(Transport transport)
        {
            _typeContext.Add(transport);
            await _typeContext.SaveChangesAsync();
            return transport;
        }

        public async Task<Transport> GetByIdAsync(string id, string profileId)
        {
            return await _typeContext.Transports.Include(x => x.TransportTags)
                .ThenInclude(x => x.Tag).Include(x => x.Reason).Include(x => x.TransportType)
                .Where(x => x.Id == id && x.UserProfileId == profileId).SingleOrDefaultAsync();
        }

        public async Task<Transport> GetByNameAsync(string name, string profileId)
        {
            return await _typeContext.Transports.Include(x => x.TransportTags)
                .ThenInclude(x => x.Tag).Include(x => x.Reason).Include(x => x.TransportType)
                .Where(x => x.Name == name && x.UserProfileId == profileId).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Transport>> GetTransportsAsync(string profileId)
        {
            return await _typeContext.Transports.Include(x => x.TransportTags)
                .ThenInclude(x => x.Tag).Include(x => x.Reason).Include(x => x.TransportType)
                .Where(x => x.UserProfileId == profileId).OrderBy(x => x.Date).ToListAsync();
        }

        public async Task<TransportDate> GetTransportsByDateAsync(DateTime date, string profileId)
        {
            IEnumerable<Transport> list = await _typeContext.Transports.Include(x => x.TransportTags).ThenInclude(x => x.Tag)
                .Include(x => x.Reason).Include(x => x.TransportType).Where(x => x.UserProfileId == profileId 
                && x.Date.Date == date.Date && x.Date.Month == date.Month && x.Date.Year == date.Year)
                .OrderBy(x => x.Date).ToListAsync();
            return new TransportDate(date, date.ToString("yyyy-MM-dd"), list);
        }

        public async Task<IEnumerable<TransportDate>> GetTransportsByRangeAsync(DateTime startDate, DateTime endDate, string profileId)
        {
            return (await _typeContext.Transports.Include(x => x.TransportTags).ThenInclude(x => x.Tag)
                .Include(x => x.Reason).Include(x => x.TransportType).Where(x => x.UserProfileId == profileId
                && x.Date.Date >= startDate && x.Date.Date <= endDate)
                .OrderBy(x => x.Date).ToListAsync()).GroupBy(x => x.Date.Date)
                .Select(x => new TransportDate(x.First().Date, x.First().Date.ToString("yyyy-MM-dd"), x));
        }

        public async Task<IEnumerable<TransportDate>> GetTransportsByWeekAsync(DateTime date, string profileId)
        {
            int dayOfWeek = (int)date.DayOfWeek;
            DateTime fDayWeek = date.AddDays(-dayOfWeek);
            DateTime lDayWeek = date.AddDays(6 - dayOfWeek);

            return (await _typeContext.Transports.Include(x => x.TransportTags).ThenInclude(x => x.Tag)
                .Include(x => x.Reason).Include(x => x.TransportType).Where(x => x.UserProfileId == profileId
                && x.Date.Date >= fDayWeek && x.Date.Date <= lDayWeek && x.Date.Month == date.Month
                && x.Date.Year == date.Year).OrderBy(x => x.Date).ToListAsync()).GroupBy(x => x.Date.Date)
                .Select(x => new TransportDate(x.First().Date, x.First().Date.ToString("yyyy-MM-dd"), x));
        }

        public async Task<IEnumerable<TransportDate>> GetTransportsByMonthAsync(DateTime date, string profileId)
        {
            return (await _typeContext.Transports.Include(x => x.TransportTags).ThenInclude(x => x.Tag)
                .Include(x => x.Reason).Include(x => x.TransportType).Where(x => x.UserProfileId == profileId
                && x.Date.Month == date.Month && x.Date.Year == date.Year).OrderBy(x => x.Date).ToListAsync()).GroupBy(x => x.Date.Date)
                .Select(x => new TransportDate(x.First().Date, x.First().Date.ToString("yyyy-MM-dd"), x));
        }

        public async Task<decimal?> GetTotalSpendByRangeAsync(DateTime startDate, DateTime endDate, string profileId)
        {
            return (await _typeContext.Transports.Where(x => x.UserProfileId == profileId
                && x.Date.Date >= startDate && x.Date.Date <= endDate)
                .OrderBy(x => x.Date).ToListAsync()).Sum(x => x.Price);
        }

        public async Task<IEnumerable<MonthlyReport>> GetTotalSpendByMonthAsync(int year, string profileId)
        {
            return (await _typeContext.Transports
                .Where(x => x.UserProfileId == profileId && x.Date.Year == year)
                .GroupBy(x => x.Date.Month)
                .Select(x => new MonthlyReport() { Month = x.Key, Total = x.Sum(s => s.Price) })
                .ToListAsync());
        }

        public async Task<Transport> RemoveAsync(Transport transport)
        {
            _typeContext.Remove(transport);
            await _typeContext.SaveChangesAsync();
            return transport;
        }

        public async Task<Transport> UpdateAsync(Transport transport)
        {
            var tagsList = await _typeContext.TransportTags
                .Where(x => x.TransportId == transport.Id).ToListAsync();

            foreach (var tag in tagsList)
            {
                if (transport.TransportTags.All(x => x.Id != tag.Id))
                {
                    _typeContext.TransportTags.Remove(tag);
                }
            }

            transport.UpdatedAt = DateTime.Now;

            Transport old = new Transport { Id = transport.Id };
            _typeContext.Transports.Attach(old);
            _typeContext.Entry(old).CurrentValues.SetValues(transport);

            await _typeContext.SaveChangesAsync();
            return transport;
        }
    }
}
