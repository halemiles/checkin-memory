using System.Collections.Generic;
using Checkin.Models;

namespace Checkin.Repositories
{
    public interface IDeviceRepository
    {
        void Create(List<Device> value);

        List<Device> GetAll();
    }
}