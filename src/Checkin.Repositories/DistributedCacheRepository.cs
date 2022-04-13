using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Checkin.Models;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Checkin.Repositories
{
    public class DistributedCacheRepository : IDistrbutedCacheRepository
    {
        private readonly IDistributedCache distributedCache;
        public DistributedCacheRepository(IDistributedCache distributedCache)
        {
            this.distributedCache = distributedCache;
        }

        public async Task<List<Device>> GetAll()
	    {
            var cacheKey = "TheTime";
            var result = await distributedCache.GetStringAsync(cacheKey);
            if(result != null)
            {
                var devices = JsonSerializer.Deserialize<List<Device>>(result);
                return devices;
            }
                return new List<Device>();
            
            

	    }

        public async Task<bool> Set(List<Device> devices)
        {
            var cacheKey = "TheTime";
            var json = JsonSerializer.Serialize(devices);
            await distributedCache.SetStringAsync(cacheKey, json);
            return true;
        }
    }
}