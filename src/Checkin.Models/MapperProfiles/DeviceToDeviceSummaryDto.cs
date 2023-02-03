using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace Checkin.Models
{
    public class DeviceToDeviceSummaryDtoProfile : Profile
    {
        public DeviceToDeviceSummaryDtoProfile()
        {
            CreateMap<DeviceSummaryDto, Device>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.IpAddress, opt => opt.MapFrom(src => src.IpAddress))                
                .ForMember(dest => dest.ModifiedDateString, opt => opt.MapFrom(src => src.ModifiedDateString))
                .ReverseMap();
        }
    }
}