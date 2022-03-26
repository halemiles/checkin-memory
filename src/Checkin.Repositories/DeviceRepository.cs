using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Checkin.Models;

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
    }
}
