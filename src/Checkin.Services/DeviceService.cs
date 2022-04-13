using System.Collections.Generic;
using Checkin.Services.Interfaces;
using Checkin.Models;
using Checkin.Repositories;
using System.Linq;
using AutoMapper;
using System.Threading.Tasks;

namespace Checkin.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly IDistrbutedCacheRepository deviceRepository;
        private readonly IMapper mapper;

        public DeviceService(
            IDistrbutedCacheRepository deviceRepository,
            IMapper mapper
        )
        {
            this.deviceRepository = deviceRepository;
            this.mapper = mapper;
        }

        public async Task<bool> Add(Device device)
        {
            //No devices exist
            var devices = await deviceRepository.GetAll() ?? new List<Device>();
            var existingDevice = devices.Find(x => x.Id == device.Id);

            //Doesnt exist
            if(!devices.Any(x => x.Id == device.Id))
            {
                 devices.Add(device);
            }
            else //Update existing
            {
                mapper.Map(device, existingDevice);
            }

            await deviceRepository.Set(devices);
            return true;
        }

        public void Update(Device device)
        {
            // var existingDevice = deviceRepository
            //                         .GetAll()
            //                         .Find(x => x.Id == device.Id);

            // var mergedDevice = mapper.Map(device, existingDevice);
            // deviceRepository.Update(mergedDevice);
        }

        public async Task<List<Device>> GetAll()
        {
            var result = await deviceRepository.GetAll();
            return result;
        }

        public Device GetByIp(string ipAddress)
        {
            // if(deviceRepository.GetAll() is List<Device> devices)
            // {
            //     return devices.Find(x => x.IpAddress == ipAddress);
            // }
            return new Device();
        }

        public Device GetByDevice(int deviceId)
        {
            // if(deviceRepository.GetAll() is List<Device> devices)
            // {
            //     return devices.Find(x => x.Id == deviceId);
            // }
            return new Device();
        }

        public void Delete(int id)
        {

        }
    }
}