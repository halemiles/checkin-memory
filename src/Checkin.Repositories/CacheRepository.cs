using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<Device> Search(int? deviceId, string ipAddress)
        {
            List<Device> existingItems = new List<Device>();
            if(memoryCache.TryGetValue(cacheKey, out List<Device> cacheItems))
            {
                existingItems = cacheItems;
            }

            if(deviceId.HasValue)
            {
                existingItems = existingItems.Where(x => x.Id == deviceId).ToList();
            }

            if(!string.IsNullOrEmpty(ipAddress))
            {
                existingItems = existingItems.Where(x => x.IpAddress == ipAddress).ToList();
            }

            return existingItems;
        }

        public bool Set(List<Device> value)
        {
            memoryCache.Set(cacheKey, value);
            return true;
        }
    }
}