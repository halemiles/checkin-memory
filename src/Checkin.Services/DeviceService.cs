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
            // --
            // --
            //TODO: Break this up. Create one key value pair for each device.
            //* We should use somthing like Device_<IpAddress>_<Name>
            // --
            // --
            var deviceKey = device.Name.ToDeviceKey();
            //No devices exist
            var existingDevice = deviceRepository.GetByKey(deviceKey.ToDeviceKey()) ?? new Device();
            // var existingDevice = devices.Find(x => x.Id == device.Id);

            // //Doesnt exist
            // if(!devices.Any(x => x.Id == device.Id))
            // {
            //     //TODO - Correct this and uncomment
            //     // logger
            //     //     .ForContext("Device",device)
            //     //     .Information("Adding new device");
            //     devices.Add(device);
            //     try
            //     {
            //         deviceRepository.Set(devices);
            //     }
            //     catch(Exception ex)
            //     {
            //         logger.Fatal(ex.ToString());
            //         return false;
            //     }
            // }
            // else //Update existing
            // {
            //     device = mapper.Map(device, existingDevice); //TODO - Unit test this call
            deviceRepository.Set(deviceKey, device); //TODO - Set an expiry time for this record
            // }

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
            //TODO: Fix this in unit tests. Null reference exception
            // logger
            //     .ForContext("DeviceId", deviceId ?? 0)
            //     .ForContext("IpAddress", ipAddress)
            //     .Information("Searching for device");
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