using System.Collections.Generic;
using Checkin.Services.Interfaces;
using Checkin.Models;

using System.Linq;
using AutoMapper;
using Serilog;
using System;
using System.Text.Json;
using Checkin.Services.Extensions;

namespace Checkin.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceCacheRepository deviceRepository;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public DeviceService(
            IDeviceCacheRepository deviceRepository,
            IMapper mapper,
            ILogger logger
        )
        {
            this.deviceRepository = deviceRepository ?? throw new ArgumentNullException(nameof(deviceRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public bool CreateOrUpdate(Device device)
        {
            try
            {
                var deviceKey = device.Name.ToDeviceKey();
                //No devices exist
                var existingDevice = deviceRepository.GetByKey(deviceKey) ?? new Device();
                
                deviceRepository.Set(deviceKey, device); 
            }
            catch(Exception err)
            {
                logger
                    .ForContext("DeviceName", device.Name)
                    .ForContext("Device IP", device.IpAddress)
                    .Fatal("Could not create or update device");
                return false;
            }

            return true;
        }

        public List<Device> GetAll()
        {
            
            logger
                .Debug("Getting all devices");
            return deviceRepository.GetAll() ?? new List<Device>();
        }

        public  List<Device> Search(Guid? deviceId, string ipAddress, string name)
        {
            logger
                .ForContext("DeviceId", deviceId ?? Guid.Empty)
                .ForContext("IpAddress", ipAddress)
                .Information("Searching for device");
            return deviceRepository.Search(deviceId, ipAddress, name.ToDeviceKey()) ?? new List<Device>();
        }

        public void Delete(string deviceName)
        {
            deviceRepository.Delete(deviceName);
        }

        public Device GetByKey(string key) //TODO - Rename key to be deviceName
        {
            return deviceRepository.GetByKey(key.ToDeviceKey()) ?? new Device();
        }
    }
}