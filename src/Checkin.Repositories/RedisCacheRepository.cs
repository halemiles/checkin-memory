using System.Collections.Generic;
using Checkin.Models;
using System.Text.Json;
using System.Linq;
using System;
using Serilog;
using Checkin.Services.Interfaces;
using StackExchange.Redis;

namespace Checkin.Repositories
{
    public class RedisCacheRepository : IDeviceCacheRepository
    {
        private readonly IConnectionMultiplexer distributedCache;
        private readonly ILogger logger;
        private IDatabase database;
        public RedisCacheRepository
        (
            IConnectionMultiplexer distributedCache,
            ILogger logger
        )
        {
            this.distributedCache = distributedCache ?? throw new ArgumentNullException(nameof(distributedCache));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            database = distributedCache.GetDatabase();
        }

        public List<Device> GetAll()
	    {
            //TODO - Consider gathering hashes and then gather the records needed
            try
            {
                var result = database.StringGet(cacheKey);
                if(!result.IsNull)
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

        public Device GetByKey(string key)
        {
            try
            {
                var result = database.StringGet(key);
                if(!result.IsNull)
                {
                    var devices = JsonSerializer.Deserialize<Device>(result);
                    return devices;
                }
            }
            catch(Exception ex)
            {
                logger
                    .ForContext("Exception",ex)
                    .Error("An exception was thrown when attempting to read from distributed cache");
            }
            return new Device();
        }

        public List<Device> Search(int? deviceId, string ipAddress)
        {
            //TODO - Might need to get all before we can search
            try
            {
                var result = database.StringGet(cacheKey);
                var devices = new List<Device>();
                if(!result.IsNull)
                {
                    devices = JsonSerializer.Deserialize<List<Device>>(result);
                }

                //TODO - Does this need checking?
                // if(deviceId.HasValue)
                // {
                //     devices = devices.Where(x => x.Id == deviceId.Value).ToList();
                // }

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

        public bool Set(string key, Device device)
        {
            try
            {
                var json = JsonSerializer.Serialize(device);
                database.StringSet(key, json);
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

        public bool Set(List<Device> devices)
        {
            //TODO - Loop through devices and update each one
            try
            {
                var json = JsonSerializer.Serialize(devices);
                database.StringSet(cacheKey, json);
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