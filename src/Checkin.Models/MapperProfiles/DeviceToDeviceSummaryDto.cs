using System;
using System.Linq;
using AutoMapper;

namespace Checkin.Models
{
    public class DeviceToDeviceSummaryDtoProfile : Profile
    {
        public DeviceToDeviceSummaryDtoProfile()
        {
            CreateMap<Device, DeviceSummaryDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.IpAddress, opt => opt.MapFrom(src => src.IpAddress))
                .ForMember(dest => dest.IspHostName, opt => opt.MapFrom(src => src.ExternalNetwork.IspName))
                .ForMember(dest => dest.Services, opt => opt.MapFrom(src => src.Services))
                .ForMember(dest => dest.BatteryLevel, opt => opt.MapFrom(src => src.Batteries.FirstOrDefault().BatteryLevel))
                .ForMember(dest => dest.ModifiedDateString, opt => opt.MapFrom(src => src.ModifiedDateString));
        }
    }
}