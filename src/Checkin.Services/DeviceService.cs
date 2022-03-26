using System.Collections.Generic;
using Checkin.Services.Interfaces;
using Checkin.Models;
using Checkin.Repositories;
using System.Linq;

namespace Checkin.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository deviceRepository;
        
        public DeviceService(
            IDeviceRepository deviceRepository
        )
        {
            this.deviceRepository = deviceRepository;
        }

        public void Add(Device device)
        {
            //No devices exist
            var devices = deviceRepository.GetAll() ?? new List<Device>();
                        
            //Doesnt exist
            if(!devices.Any(x => x.IpAddress == device.IpAddress))
            {
                 devices.Add(device);
            }
            else //Update existing
            {
                var existing = devices.Find(x => x.IpAddress == device.IpAddress);
                existing.Name = device.Name;
                existing.CreatedDate = device.CreatedDate;
                existing.ModifiedDate = device.ModifiedDate;
            }

            deviceRepository.Create(devices);
        }

        public List<Device> GetAll()
        {
            if(deviceRepository.GetAll() is List<Device> devices)
            {
                return devices;
            }
            return new List<Device>();
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
    }
}