using System;
using System.Collections.Generic;
using System.Linq;
using Checkin.Models;
using Microsoft.Extensions.Caching.Memory;
using Checkin.Services.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace Checkin.Repositories
{
    [ExcludeFromCodeCoverage]
    public class DeviceCacheRepository : IDeviceCacheRepository
    {
        private readonly string cacheKey = "Devices";
        private readonly IMemoryCache memoryCache;

        public DeviceCacheRepository(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        public void Delete(string deviceName)
        {
            throw new NotImplementedException();
        }

        public List<Device> GetAll()
        {
            if(memoryCache.TryGetValue(cacheKey, out List<Device> cacheItems))
            {
                return cacheItems;
            }

            return new List<Device>();
        }

        public Device GetByKey(string key)
        {
            List<Device> existingItems = new();
            if(memoryCache.TryGetValue(cacheKey, out List<Device> cacheItems))
            {
                existingItems = cacheItems;
            }

            return existingItems.FirstOrDefault(x => x.Key == key);
        }

        public List<Device> Search(Guid? deviceId, string ipAddress, string name)
        {
            List<Device> existingItems = new();
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

        public bool Set(string key, Device value)
        {
            throw new NotImplementedException();
        }
    }
}