using System;
using System.Collections.Generic;
using Checkin.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Checkin.Repositories
{
    public class DeviceCacheRepository : IDeviceCacheRepository
    {
        private readonly string cacheKey = "Devices";
        private readonly IMemoryCache memoryCache;

        public DeviceCacheRepository(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        public List<Device> GetAll()
        {
            if(memoryCache.TryGetValue(cacheKey, out List<Device> cacheItems))
            {
                return cacheItems;
            }

            return new List<Device>();
        }

        public bool Set(List<Device> value)
        {
            memoryCache.Set(cacheKey, value);
            return true;
        }
    }
}