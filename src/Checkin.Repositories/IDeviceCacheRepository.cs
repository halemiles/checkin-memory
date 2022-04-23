using System.Collections.Generic;
using Checkin.Models;

namespace Checkin.Repositories
{
    public interface IDeviceCacheRepository
    {
        bool Set(List<Device> value);

        List<Device> GetAll();
        List<Device> Search(int? deviceId, string ipAddress);
    }
}