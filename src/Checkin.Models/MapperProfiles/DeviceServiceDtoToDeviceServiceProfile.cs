using System;
using AutoMapper;

namespace Checkin.Models
{
    public class ServiceStatusDtoToServiceStatusProfile : Profile
    {
        public ServiceStatusDtoToServiceStatusProfile()
        {
            CreateMap<ServiceStatusDto, ServiceStatus>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
        }
    }

    public class ServiceStatusToServiceStatusDtoProfile : Profile
    {
        public ServiceStatusToServiceStatusDtoProfile()
        {
            CreateMap<ServiceStatus, ServiceStatusDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
        }
    }
}