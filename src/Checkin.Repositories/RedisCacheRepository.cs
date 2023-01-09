using System.Collections.Generic;
using Checkin.Models;
using System.Text.Json;
using System.Linq;
using System;
using Serilog;
using Checkin.Services.Interfaces;
using StackExchange.Redis;
using System.Net;

namespace Checkin.Repositories
{
    public class RedisCacheRepository : IDeviceCacheRepository
    {
        private readonly ILogger logger;
        private readonly IDatabase database;
        private readonly IEnumerable<RedisKey> keys;
        private readonly IConnectionMultiplexer cache;
        public RedisCacheRepository
        (
            IConnectionMultiplexer distributedCache,
            ILogger logger
        )
        {
            cache = distributedCache;
            database = distributedCache?.GetDatabase() ?? throw new ArgumentNullException(nameof(distributedCache));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public List<Device> GetAll()
	    {
            List<Device> devices = new();
            try
            {                
                foreach(var key in GetKeys())
                {
                    var result = database.StringGet(key);
                    if(!result.IsNull)
                    {
                        var device = new Device();
                        try
                        {
                            device = JsonSerializer.Deserialize<Device>(result);
                            devices.Add(device);
                        }
                        catch(Exception valEx)
                        {
                            logger
                                .ForContext("Exception", valEx)
                                .Error("Could not parse JSON"); 
                            
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                logger
                    .ForContext("Exception",ex)
                    .Error("An exception was thrown when attempting to read from distributed cache");
            }
            return devices;
	    }

        private List<RedisKey> GetKeys()
        {
            EndPoint endPoint = cache.GetEndPoints().First();
            var keys = cache.GetServer(endPoint).Keys(pattern: "*").ToList();
            return keys;
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

        public List<Device> Search(Guid? deviceId, string ipAddress, string name)
        {
            //TODO - Might need to get all before we can search
            try
            {
                var result = database.StringGet(name);  //TODO - Use a key for this
                var devices = new Device();
                if(!result.IsNull)
                {
                    devices = JsonSerializer.Deserialize<Device>(result);
                }

                //TODO - Does this need checking?
                // if(deviceId.HasValue)
                // {
                //     devices = devices.Where(x => x.Id == deviceId.Value).ToList();
                // }

                // if(!string.IsNullOrEmpty(ipAddress))
                // {
                //     devices = devices.Where(x => x.IpAddress == ipAddress).ToList();
                // }
                return new List<Device> { devices };
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
                database.StringSet(string.Empty, json); //TODO - Use a key for this
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

        public void Delete(string deviceName)
        {
            database.KeyDelete(deviceName);
        }
    }
}