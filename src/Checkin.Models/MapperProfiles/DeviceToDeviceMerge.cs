using AutoMapper;

namespace Checkin.Models
{
    public class DeviceToDeviceMergeProfile : Profile
    {
        public DeviceToDeviceMergeProfile()
        {
            CreateMap<Device, Device>()
                .ForAllMembers(o => o.Condition((source, destination, member) => member != null));
        }
    }
}