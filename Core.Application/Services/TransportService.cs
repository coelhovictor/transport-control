using AutoMapper;
using Core.Application.DTOs;
using Core.Application.Interfaces;
using Core.Domain.Entities;
using Core.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Application.Services
{
    public class TransportService : ITransportService
    {
        private ITransportRepository _transportRepository;
        private readonly IMapper _mapper;

        public TransportService(ITransportRepository transportRepository, IMapper mapper)
        {
            _transportRepository = transportRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(TransportDTOSet transportDTO, UserProfileDTOGet profileDTO)
        {
            var transportEntity = _mapper.Map<Transport>(transportDTO);
            transportEntity.UserProfileId = profileDTO.Id;

            await _transportRepository.CreateAsync(transportEntity);
        }

        public async Task<TransportDTOGet> GetByIdAsync(string id, UserProfileDTOGet profileDTO)
        {
            var transportDTO = await _transportRepository.GetByIdAsync(id, profileDTO.Id);
            return _mapper.Map<TransportDTOGet>(transportDTO);
        }

        public async Task<TransportDTOGet> GetByNameAsync(string name, UserProfileDTOGet profileDTO)
        {
            var transportDTO = await _transportRepository.GetByNameAsync(name, profileDTO.Id);
            return _mapper.Map<TransportDTOGet>(transportDTO);
        }

        public async Task<IEnumerable<TransportDTOGet>> GetTransportsAsync(UserProfileDTOGet profileDTO)
        {
            var transportEntity = await _transportRepository.GetTransportsAsync(profileDTO.Id);
            return _mapper.Map<IEnumerable<TransportDTOGet>>(transportEntity);
        }

        public async Task<TransportDateDTO> GetTransportsByDateAsync(DateTime date, UserProfileDTOGet profileDTO)
        {
            var transportEntity = await _transportRepository.GetTransportsByDateAsync(date, profileDTO.Id);
            return _mapper.Map<TransportDateDTO>(transportEntity);
        }

        public async Task<IEnumerable<TransportDateDTO>> GetTransportsByRangeAsync(DateTime startDate, DateTime endDate, UserProfileDTOGet profileDTO)
        {
            var transportEntity = await _transportRepository.GetTransportsByRangeAsync(startDate, endDate, profileDTO.Id);
            return _mapper.Map<IEnumerable<TransportDateDTO>>(transportEntity);
        }

        public async Task<IEnumerable<TransportDateDTO>> GetTransportsByWeekAsync(DateTime date, UserProfileDTOGet profileDTO)
        {
            var transportEntity = await _transportRepository.GetTransportsByWeekAsync(date, profileDTO.Id);
            return _mapper.Map<IEnumerable<TransportDateDTO>>(transportEntity);
        }

        public async Task<IEnumerable<TransportDateDTO>> GetTransportsByMonthAsync(DateTime date, UserProfileDTOGet profileDTO)
        {
            var transportEntity = await _transportRepository.GetTransportsByMonthAsync(date, profileDTO.Id);
            return _mapper.Map<IEnumerable<TransportDateDTO>>(transportEntity);
        }

        public async Task<decimal?> GetTotalSpendByRangeAsync(DateTime startDate, DateTime endDate, UserProfileDTOGet profileDTO)
        {
            return await _transportRepository.GetTotalSpendByRangeAsync(startDate, endDate, profileDTO.Id);
        }

        public async Task<decimal?> GetTotalSpendThisWeekAsync(UserProfileDTOGet profileDTO)
        {
            int dayOfWeek = DateTime.Now.DayOfWeek.GetHashCode();

            DateTime min = DateTime.Now.AddDays(-dayOfWeek);
            DateTime max = DateTime.Now.AddDays(6-dayOfWeek);

            DateTime startDate = new DateTime(min.Year, min.Month, min.Day, 00, 00, 00);
            DateTime endDate = new DateTime(max.Year, max.Month, max.Day, 23, 59, 59);

            return await _transportRepository.GetTotalSpendByRangeAsync(startDate, endDate, profileDTO.Id);
        }

        public async Task<decimal?> GetTotalSpendLastWeekAsync(UserProfileDTOGet profileDTO)
        {
            int dayOfWeek = DateTime.Now.AddDays(-7).DayOfWeek.GetHashCode();

            DateTime min = DateTime.Now.AddDays(-7).AddDays(-dayOfWeek);
            DateTime max = DateTime.Now.AddDays(-7).AddDays(6 - dayOfWeek);

            DateTime startDate = new DateTime(min.Year, min.Month, min.Day, 00, 00, 00);
            DateTime endDate = new DateTime(max.Year, max.Month, max.Day, 23, 59, 59);

            return await _transportRepository.GetTotalSpendByRangeAsync(startDate, endDate, profileDTO.Id);
        }

        public async Task<decimal?> GetTotalSpendThisMonthAsync(UserProfileDTOGet profileDTO)
        {
            DateTime min = DateTime.Now;
            DateTime max = DateTime.Now.AddMonths(1);

            DateTime startDate = new DateTime(min.Year, min.Month, 1, 00, 00, 00);
            DateTime endDate = new DateTime(max.Year, max.Month, 1, 23, 59, 59).AddDays(-1);

            return await _transportRepository.GetTotalSpendByRangeAsync(startDate, endDate, profileDTO.Id);
        }

        public async Task<decimal?> GetTotalSpendLastMonthAsync(UserProfileDTOGet profileDTO)
        {
            DateTime min = DateTime.Now.AddMonths(-1);
            DateTime max = DateTime.Now;

            DateTime startDate = new DateTime(min.Year, min.Month, 1, 00, 00, 00);
            DateTime endDate = new DateTime(max.Year, max.Month, 1, 23, 59, 59).AddDays(-1);

            return await _transportRepository.GetTotalSpendByRangeAsync(startDate, endDate, profileDTO.Id);
        }

        public async Task<decimal?> GetTotalSpendThisYearAsync(UserProfileDTOGet profileDTO)
        {
            DateTime min = DateTime.Now;
            DateTime max = DateTime.Now.AddYears(1);

            DateTime startDate = new DateTime(min.Year, 1, 1, 00, 00, 00);
            DateTime endDate = new DateTime(max.Year, 1, 1, 23, 59, 59).AddDays(-1);

            return await _transportRepository.GetTotalSpendByRangeAsync(startDate, endDate, profileDTO.Id);
        }

        public async Task<IEnumerable<MonthlyReportDTO>> GetTotalSpendByMonthAsync(int year, UserProfileDTOGet profileDTO)
        {
            var reportEntity = await _transportRepository.GetTotalSpendByMonthAsync(year, profileDTO.Id);
            return _mapper.Map<IEnumerable<MonthlyReportDTO>>(reportEntity);
        }

        public async Task RemoveAsync(string id, UserProfileDTOGet profileDTO)
        {
            var transportEntity = _transportRepository.GetByIdAsync(id, profileDTO.Id).Result;
            await _transportRepository.RemoveAsync(transportEntity);
        }

        public async Task UpdateAsync(TransportDTOSet transportDTO, UserProfileDTOGet profileDTO)
        {
            var transportEntity = _mapper.Map<Transport>(transportDTO);
            transportEntity.UserProfileId = profileDTO.Id;

            await _transportRepository.UpdateAsync(transportEntity);
        }
    }
}
