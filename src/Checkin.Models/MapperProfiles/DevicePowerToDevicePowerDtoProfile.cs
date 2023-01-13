using System;
using AutoMapper;

namespace Checkin.Models
{
    public class DevicePowerToDevicePowerDtoProfile : Profile
    {
        public DevicePowerToDevicePowerDtoProfile()
        {
            CreateMap<DevicePowerDto, DevicePower>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.Batteries, opt => opt.MapFrom(src => src.Batteries))
                .ReverseMap();
        }
    }
}