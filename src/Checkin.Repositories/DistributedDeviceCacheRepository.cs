using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Checkin.Models;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Checkin.Repositories
{
    public class DistributedDeviceCacheRepository : IDeviceCacheRepository
    {
        private readonly string cacheKey = "Devices";
        private readonly IDistributedCache distributedCache;
        public DistributedDeviceCacheRepository(IDistributedCache distributedCache)
        {
            this.distributedCache = distributedCache;
        }

        public List<Device> GetAll()
	    {
            var result = distributedCache.GetString(cacheKey);
            if(result != null)
            {
                var devices = JsonSerializer.Deserialize<List<Device>>(result);
                return devices;
            }
                return new List<Device>();
            
            

	    }

        public bool Set(List<Device> devices)
        {
            var json = JsonSerializer.Serialize(devices);
            distributedCache.SetString(cacheKey, json);
            return true;
        }
    }
}