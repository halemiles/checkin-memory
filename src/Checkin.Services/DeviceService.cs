using System.Collections.Generic;
using Checkin.Services.Interfaces;
using Checkin.Models;
using Checkin.Repositories;
using System.Linq;
using AutoMapper;
using Serilog;
using System;
using System.Text.Json;

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
            this.logger = logger ?? throw new ArgumentNullException(nameof(mapper));
        }

        public bool Add(Device device)
        {
            //No devices exist
            var devices = deviceRepository.GetAll() ?? new List<Device>();
            var existingDevice = devices.Find(x => x.Id == device.Id);

            //Doesnt exist
            if(!devices.Any(x => x.Id == device.Id))
            {
                //TODO - Correct this and uncomment
                // logger
                //     .ForContext("Device",device)
                //     .Information("Adding new device");
                devices.Add(device);
                try
                {
                    deviceRepository.Set(devices);
                }
                catch(Exception ex)
                {
                    Log.Fatal(ex.ToString());
                    return false;
                }
            }
            else //Update existing
            {
                return Update(device);
            }

            return true;
        }

        public bool Update(Device device)
        {
            try{
            var devices = deviceRepository.GetAll();
            var existingDevice = devices
                                    .Find(x => x.Id == device.Id);

            var mergedDevice = mapper.Map(device, existingDevice);
            logger
                .ForContext("Device", device)
                .Information($"Updating device {device.Name}");
            deviceRepository.Set(devices);
            }
            catch(Exception ex)
            {
                Log.Fatal(ex.ToString());
                return false;
            }
            return true;
        }

        public List<Device> GetAll()
        {
            logger
                .Information("Getting all devices");
            return deviceRepository.GetAll() ?? new List<Device>();
        }

        public  List<Device> Search(int? deviceId, string ipAddress)
        {
            logger
                .ForContext("DeviceId", deviceId)
                .ForContext("IpAddress", ipAddress)
                .Information("Searching for device");
            return deviceRepository.Search(deviceId, ipAddress) ?? new List<Device>();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}