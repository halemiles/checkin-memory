using System.Collections.Generic;
using Checkin.Models;
using System.Text.Json;
using System.Linq;
using System;
using Serilog;
using Checkin.Services.Interfaces;
using StackExchange.Redis;
using System.Net;
using Checkin.Services.Extensions;
using System.Threading.Tasks;

namespace Checkin.Repositories
{
    public class RedisDeviceCacheRepository : IDeviceCacheRepository
    {
        private readonly ILogger logger;
        private readonly IDatabase database;
        private readonly IEnumerable<RedisKey> keys;
        private readonly IConnectionMultiplexer cache;
        public RedisDeviceCacheRepository
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

        public async Task<Device> GetByKey(string key)
        {
            try
            {
                
                var result = await database.StringGetAsync(key);
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

        public List<Device> Search(SearchDto searchDto)
        {
            try
            {
                var allDevices = GetAll();
                
                var result = new List<Device>();
                if(searchDto.DeviceId.HasValue)
                {
                    result = allDevices.Where(x => x.Id == searchDto.DeviceId.Value).ToList();
                }

                if(!string.IsNullOrEmpty(searchDto.IpAddress))
                {
                    result = allDevices.Where(x => x.IpAddress == searchDto.IpAddress).ToList();
                }

                if(!string.IsNullOrEmpty(searchDto.DeviceName))
                {
                    result = allDevices.Where(x => x.Key == searchDto.DeviceName.ToDeviceKey()).ToList();
                }               

                return result;
            }
            catch(Exception ex)
            {
                logger
                    .ForContext("Exception",ex)
                    .Error("An exception was thrown when attempting to search from distributed cache");
            }

            return new List<Device>();
        }

        public async Task<bool> Set(string key, Device device)
        {
            try
            {
                var json = JsonSerializer.Serialize(device);
                await database.StringSetAsync(key, json);
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