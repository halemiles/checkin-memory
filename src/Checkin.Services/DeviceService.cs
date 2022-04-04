using System.Collections.Generic;
using Checkin.Services.Interfaces;
using Checkin.Models;
using Checkin.Repositories;
using System.Linq;
using AutoMapper;

namespace Checkin.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository deviceRepository;
        private readonly IMapper mapper;
        
        public DeviceService(
            IDeviceRepository deviceRepository,
            IMapper mapper
        )
        {
            this.deviceRepository = deviceRepository;
            this.mapper = mapper;
        }

        public void Add(Device device)
        {
            //No devices exist
            var devices = deviceRepository.GetAll() ?? new List<Device>();
                        
            //Doesnt exist
            if(!devices.Any(x => x.Id == device.Id))
            {
                 devices.Add(device);
            }
            else //Update existing
            {
                var existing = devices.Find(x => x.Id == device.Id);
                mapper.Map(device, existing);
            }

            deviceRepository.Create(devices);
        }



        public void Update(Device device)
        {
            var existingDevice = deviceRepository.GetAll().FirstOrDefault(x => x.Id == device.Id);
            
            var mergedDevice = mapper.Map(device, existingDevice);
            deviceRepository.Update(mergedDevice);
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