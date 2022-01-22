using Core.Domain.Entities;
using Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Domain.Interfaces
{
    public interface ITransportRepository
    {
        Task<IEnumerable<Transport>> GetTransportsAsync(string profileId);
        Task<TransportDate> GetTransportsByDateAsync(DateTime date, string profileId);
        Task<IEnumerable<TransportDate>> GetTransportsByRangeAsync(DateTime startDate, DateTime endDate, string profileId);
        Task<IEnumerable<TransportDate>> GetTransportsByWeekAsync(DateTime date, string profileId);
        Task<IEnumerable<TransportDate>> GetTransportsByMonthAsync(DateTime date, string profileId);
        Task<decimal?> GetTotalSpendByRangeAsync(DateTime startDate, DateTime endDate, string profileId);
        Task<IEnumerable<MonthlyReport>> GetTotalSpendByMonthAsync(int year, string profileId);
        Task<Transport> GetByIdAsync(string id, string profileId);
        Task<Transport> GetByNameAsync(string name, string profileId);

        Task<Transport> CreateAsync(Transport transport);
        Task<Transport> UpdateAsync(Transport transport);
        Task<Transport> RemoveAsync(Transport transport);
    }
}
