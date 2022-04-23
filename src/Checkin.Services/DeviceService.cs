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

        public DeviceService(
            IDeviceCacheRepository deviceRepository,
            IMapper mapper
        )
        {
            this.deviceRepository = deviceRepository ?? throw new ArgumentNullException(nameof(deviceRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public bool Add(Device device)
        {
            //No devices exist
            var devices = deviceRepository.GetAll() ?? new List<Device>();
            var existingDevice = devices.Find(x => x.Id == device.Id);

            //Doesnt exist
            if(!devices.Any(x => x.Id == device.Id))
            {
                Log.Information("{timestamp} Adding new device {device}", DateTime.Now.ToShortTimeString(), JsonSerializer.Serialize(device));
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
            Log.Information("{timestamp} Updating device {device} with values {values}", DateTime.Now.ToShortTimeString(), device.Name, JsonSerializer.Serialize(device));
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
            return deviceRepository.GetAll() ?? new List<Device>();
        }

        public Device GetByIp(string ipAddress)
        {
            if(deviceRepository.GetAll() is List<Device> devices)
            {
                return devices.Find(x => x.IpAddress == ipAddress);
            }
            return new Device();
        }

        public Device GetByDevice(int deviceId)
        {
            if(deviceRepository.GetAll() is List<Device> devices)
            {
                return devices.Find(x => x.Id == deviceId);
            }
            return new Device();
        }

        public void Delete(int id)
        {

        }
    }
}