using System.Collections.Generic;
using Checkin.Models;

namespace Checkin.Repositories
{
    public interface IDeviceRepository
    {
        void Create(List<Device> value);
        void Update(Device device);

        List<Device> GetAll();
    }
}