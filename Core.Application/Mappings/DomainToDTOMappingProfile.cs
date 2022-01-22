using AutoMapper;
using Core.Application.DTOs;
using Core.Domain.Entities;
using Core.Domain.Models;

namespace Core.Application.Mappings
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<UserProfile, UserProfileDTOGet>().ReverseMap();
            CreateMap<UserProfile, UserProfileDTOSet>().ReverseMap();

            CreateMap<TransportType, TransportTypeDTOGet>().ReverseMap();
            CreateMap<TransportType, TransportTypeDTOSet>().ReverseMap();

            CreateMap<Reason, ReasonDTOGet>().ReverseMap();
            CreateMap<Reason, ReasonDTOSet>().ReverseMap();

            CreateMap<Tag, TagDTOGet>().ReverseMap();
            CreateMap<Tag, TagDTOSet>().ReverseMap();

            CreateMap<Transport, TransportDTOGet>().ReverseMap();
            CreateMap<Transport, TransportDTOSet>().ReverseMap();

            CreateMap<TransportDate, TransportDateDTO>().ReverseMap();
            CreateMap<MonthlyReport, MonthlyReportDTO>().ReverseMap();

            CreateMap<TransportTag, TransportTagDTOGet>().ReverseMap();
            CreateMap<TransportTag, TransportTagDTOSet>().ReverseMap();
        }
    }
}
