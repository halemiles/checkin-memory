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
                var existingDevice = deviceRepository.GetByKey(deviceKey);
                
                if(existingDevice.Id == Guid.Empty)
                {
                    logger
                        .ForContext("DeviceName", device.Name)
                        .ForContext("DeviceIP", device.IpAddress)
                        .Information("Device does not exist. We will try and create one");
                    device.Id = Guid.NewGuid();
                }
                
                deviceRepository.Set(deviceKey, device); 
            }
            catch(Exception err)
            {
                logger
                    .ForContext("DeviceName", device.Name)
                    .ForContext("Device IP", device.IpAddress)
                    .ForContext("Exception", err.ToString())
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
            deviceRepository.Delete(deviceName.ToDeviceKey());
        }
    }
}