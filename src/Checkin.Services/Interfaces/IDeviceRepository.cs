using System.Collections.Generic;
using Checkin.Models;

namespace Checkin.Services.Interfaces
{
    public interface IDeviceRepository
    {
        void Create(List<Device> value);
        void Update(Device device);

        List<Device> GetAll();
    }
}