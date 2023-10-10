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
using System.Diagnostics.CodeAnalysis;

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


        //TODO - Finish this
        public void Delete(string deviceName)
        {
            throw new NotImplementedException();
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


        //TODO - Do we need this anymore?
        public Device GetByKey(string key)
        {
            throw new NotImplementedException();
        }

        public List<Device> Search(SearchDto searchDto)
        {
            try
            {
                var result = distributedCache.GetString(cacheKey);
                var devices = new List<Device>();
                if(result != null)
                {
                    devices = JsonSerializer.Deserialize<List<Device>>(result);
                }

                if(searchDto.DeviceId.HasValue)
                {
                    devices = devices.Where(x => x.Id == searchDto.DeviceId.Value).ToList();
                }

                if(!string.IsNullOrEmpty(searchDto.IpAddress))
                {
                    devices = devices.Where(x => x.IpAddress == searchDto.IpAddress).ToList();
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

        //TODO - Do we need this anymore?
        public bool Set(string key, Device value)
        {
            throw new NotImplementedException();
        }
    }
}