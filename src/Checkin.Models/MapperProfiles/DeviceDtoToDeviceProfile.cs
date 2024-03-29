using System;
using AutoMapper;
using Checkin.Models.Dto;

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
                .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.ExternalNetwork, opt => opt.MapFrom(src => src.ExternalNetwork))
                .ForMember(dest => dest.Services, opt => opt.MapFrom(src => src.Services))
                .ForMember(dest => dest.Power, opt => opt.MapFrom(src => src.Power))
                .ForMember(dest => dest.ModifiedDateString, opt => opt.MapFrom(src => src.ModifiedDateString))
                .ForMember(dest => dest.Attributes, opt => opt.MapFrom(src => src.Attributes))
                .ReverseMap();
        }
    }
}