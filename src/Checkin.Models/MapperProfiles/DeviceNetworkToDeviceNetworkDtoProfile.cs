using System;
using AutoMapper;

namespace Checkin.Models
{
    public class DeviceNetworkToDeviceNetworkDtoProfile : Profile
    {
        public DeviceNetworkToDeviceNetworkDtoProfile()
        {
            CreateMap<DeviceNetworkDto, DeviceNetwork>()
                .ForMember(dest => dest.ExternalIpAddress, opt => opt.MapFrom(src => src.ExternalIpAddress))
                .ForMember(dest => dest.IspName, opt => opt.MapFrom(src => src.IspName))
                .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => src.LastModified))
                .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => src.DateCreated))
                .ReverseMap();
        }
    }
}