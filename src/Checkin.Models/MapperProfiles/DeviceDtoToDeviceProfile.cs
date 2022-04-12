using System;
using AutoMapper;

namespace Checkin.Models
{
    public class DeviceDtoToDeviceProfile : Profile
    {
        public DeviceDtoToDeviceProfile()
        {
            CreateMap<DeviceDto, Device>()            
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.IpAddress, opt => opt.MapFrom(src => src.IpAddress))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.ExternalNetwork, opt => opt.MapFrom(src => src.ExternalNetwork))
                .ForMember(dest => dest.Services, opt => opt.MapFrom(src => src.Services))
                .ReverseMap();
        }
    }
}