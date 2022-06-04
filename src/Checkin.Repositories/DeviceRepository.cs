using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Checkin.Models;
using System.Linq;
using Serilog;
using Checkin.Services.Interfaces;

namespace Checkin.Repositories
{
    public class LocalDeviceRepository : IDeviceRepository
    {
        private readonly IDeviceCacheRepository cache;
        private readonly ILogger logger;
        public LocalDeviceRepository
        (
            IDeviceCacheRepository cache,
            ILogger logger
        )
        {
            this.cache = cache ?? throw new ArgumentNullException(nameof(cache));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Create(List<Device> value)
        {
            try
            {
                cache.Set(value);
            }
            catch(Exception ex)
            {
                logger
                    .ForContext("Exception",ex)
                    .Error("An exception was thrown when attempting to read from distributed cache");
            
            }
        }

        public List<Device> GetAll()
        {
            var result = cache.GetAll();
            return result;
        }

        public void Update(Device device)
        {
            var devices = cache.GetAll();
            var existing = devices.FirstOrDefault(x => x.Id == device.Id) ?? new Device();
            existing = device; //TODO - Refactor this. Use pattern matching?
            cache.Set(devices);
        }
    }
}
