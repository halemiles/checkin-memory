using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Checkin.Models;
using System.Linq;

namespace Checkin.Repositories
{
    public class DeviceRepository : IDeviceRepository
    {
        private const string CacheKey = "Devices";
        private ICacheRepository<List<Device>> cache;
        public DeviceRepository(ICacheRepository<List<Device>> cache)
        {
            this.cache = cache;
        }

        public void Create(List<Device> value)
        {
            cache.Set(CacheKey, value);
        }

        public List<Device> GetAll()
        {
            return cache.Get(CacheKey);
        }

        public void Update(Device device)
        {
            var devices = cache.Get(CacheKey);
            var existing = devices.SingleOrDefault(x => x.Id == device.Id);
            existing = device;
            cache.Set(CacheKey, devices);
        }
    }
}
