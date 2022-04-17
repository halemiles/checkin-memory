using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Checkin.Models;
using System.Linq;

namespace Checkin.Repositories
{
    public class LocalDeviceRepository : IDeviceRepository
    {
        private readonly IDeviceCacheRepository cache;
        public LocalDeviceRepository(IDeviceCacheRepository cache)
        {
            this.cache = cache;
        }

        public void Create(List<Device> value)
        {
            cache.Set(value);
        }

        public List<Device> GetAll()
        {
            var result = cache.GetAll();
            return result;
        }

        public void Update(Device device)
        {
            var devices = cache.GetAll();
            var existing = devices.FirstOrDefault(x => x.Id == device.Id);
            existing = device;
            cache.Set(devices);
        }
    }
}
