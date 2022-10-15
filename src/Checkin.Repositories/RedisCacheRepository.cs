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
        private readonly ILogger logger;
        private readonly IDatabase database;
        private readonly IEnumerable<RedisKey> keys;
        public RedisCacheRepository
        (
            IConnectionMultiplexer distributedCache,
            ILogger logger
        )
        {
            database = distributedCache?.GetDatabase() ?? throw new ArgumentNullException(nameof(distributedCache));
            var server = distributedCache.GetServer("localhost",6379);
            keys = server.Keys();
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public List<Device> GetAll()
	    {
            List<Device> devices = new();
            //TODO - Consider gathering hashes and then gather the records needed
            try
            {
                foreach(var key in keys)
                {
                    var result = database.StringGet(key);  //TODO - Use a key for this
                    if(!result.IsNull)
                    {
                        var device = JsonSerializer.Deserialize<Device>(result);
                        devices.Add(device);
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

        public List<Device> Search(Guid? deviceId, string ipAddress)
        {
            //TODO - Might need to get all before we can search
            try
            {
                var result = database.StringGet(string.Empty);  //TODO - Use a key for this
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