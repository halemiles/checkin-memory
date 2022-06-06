using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Checkin.Models;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Linq;
using System;
using Serilog;
using Serilog.Exceptions;
using Checkin.Services.Interfaces;

namespace Checkin.Repositories
{
    public class DistributedDeviceCacheRepository : IDeviceCacheRepository
    {
        private readonly string cacheKey = "Devices";
        private readonly IDistributedCache distributedCache;
        private readonly ILogger logger;
        public DistributedDeviceCacheRepository
        (
            IDistributedCache distributedCache,
            ILogger logger
        )
        {
            this.distributedCache = distributedCache ?? throw new ArgumentNullException(nameof(distributedCache));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public List<Device> GetAll()
	    {
            try
            {
                var result = distributedCache.GetString(cacheKey);
                if(result != null)
                {
                    var devices = JsonSerializer.Deserialize<List<Device>>(result);
                    return devices;
                }
            }
            catch(Exception ex)
            {
                logger
                    .ForContext("Exception",ex)
                    .Error("An exception was thrown when attempting to read from distributed cache");
            }
            return new List<Device>();
	    }

        public List<Device> Search(int? deviceId, string ipAddress)
        {
            try
            {
                var result = distributedCache.GetString(cacheKey);
                var devices = new List<Device>();
                if(result != null)
                {
                    devices = JsonSerializer.Deserialize<List<Device>>(result);
                }

                if(deviceId.HasValue)
                {
                    devices = devices.Where(x => x.Id == deviceId.Value).ToList();
                }

                if(!string.IsNullOrEmpty(ipAddress))
                {
                    devices = devices.Where(x => x.IpAddress == ipAddress).ToList();
                }
                return devices;
            }
            catch(Exception ex)
            {
                logger
                    .ForContext("Exception",ex)
                    .Error("An exception was thrown when attempting to search from distributed cache");
            }

            return new List<Device>();
        }

        public bool Set(List<Device> devices)
        {
            try
            {
                var json = JsonSerializer.Serialize(devices);
                distributedCache.SetString(cacheKey, json);
            }
            catch(Exception ex)
            {
                logger
                    .ForContext("Exception",ex)
                    .Error("An exception was thrown when attempting to set distributed cache");
                return false;
            }
            return true;
        }
    }
}