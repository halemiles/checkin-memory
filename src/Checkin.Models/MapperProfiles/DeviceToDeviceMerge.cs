using AutoMapper;

namespace Checkin.Models
{
    public class DeviceToDeviceMergeProfile : Profile
    {
        public DeviceToDeviceMergeProfile()
        {
            CreateMap<Device, Device>()
                .ForAllMembers(o => o.Condition((_, _, member) => member != null));
        }
    }
}