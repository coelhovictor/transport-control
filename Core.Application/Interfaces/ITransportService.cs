using Core.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Application.Interfaces
{
    public interface ITransportService
    {
        Task<IEnumerable<TransportDTOGet>> GetTransportsAsync(UserProfileDTOGet profileDTO);
        Task<TransportDateDTO> GetTransportsByDateAsync(DateTime date, UserProfileDTOGet profileDTO);
        Task<IEnumerable<TransportDateDTO>> GetTransportsByRangeAsync(DateTime startDate, DateTime endDate, UserProfileDTOGet profileDTO);
        Task<IEnumerable<TransportDateDTO>> GetTransportsByWeekAsync(DateTime date, UserProfileDTOGet profileDTO);
        Task<IEnumerable<TransportDateDTO>> GetTransportsByMonthAsync(DateTime date, UserProfileDTOGet profileDTO);
        Task<decimal?> GetTotalSpendByRangeAsync(DateTime startDate, DateTime endDate, UserProfileDTOGet profileDTO);
        Task<decimal?> GetTotalSpendThisWeekAsync(UserProfileDTOGet profileDTO);
        Task<decimal?> GetTotalSpendLastWeekAsync(UserProfileDTOGet profileDTO);
        Task<decimal?> GetTotalSpendThisMonthAsync(UserProfileDTOGet profileDTO);
        Task<decimal?> GetTotalSpendLastMonthAsync(UserProfileDTOGet profileDTO);
        Task<decimal?> GetTotalSpendThisYearAsync(UserProfileDTOGet profileDTO);
        Task<IEnumerable<MonthlyReportDTO>> GetTotalSpendByMonthAsync(int year, UserProfileDTOGet profileDTO);
        Task<TransportDTOGet> GetByIdAsync(string id, UserProfileDTOGet profileDTO);
        Task<TransportDTOGet> GetByNameAsync(string name, UserProfileDTOGet profileDTO);
        Task AddAsync(TransportDTOSet transportDTO, UserProfileDTOGet profileDTO);
        Task UpdateAsync(TransportDTOSet transportDTO, UserProfileDTOGet profileDTO);
        Task RemoveAsync(string id, UserProfileDTOGet profileDTO);
    }
}
