using System;
using AutoMapper;

namespace Checkin.Models
{
    public class DeviceBatterToDeviceBatteryDtoProfile : Profile
    {
        public DeviceBatterToDeviceBatteryDtoProfile()
        {
            CreateMap<DeviceBatteryDto, DeviceBattery>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.BatteryLevel, opt => opt.MapFrom(src => src.BatteryLevel))
                .ReverseMap();
        }
    }
}