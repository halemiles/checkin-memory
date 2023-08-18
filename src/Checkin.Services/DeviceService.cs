using System.Collections.Generic;
using Checkin.Services.Interfaces;
using Checkin.Models;

using System.Linq;
using AutoMapper;
using Serilog;
using System;
using System.Text.Json;
using Checkin.Services.Extensions;
using System.Threading.Tasks;

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

        public async Task<ApiResponse<bool>> CreateOrUpdate(Device device)
        {
            try
            {
                var deviceKey = device.Name.ToDeviceKey();
                var existingDevice = await deviceRepository.GetByKey(deviceKey);
                
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
                return new ApiResponse<bool>("Could not create or update device", false, ResultCode.INTERNALERROR);
            }

            return new ApiResponse<bool>( "Crated device", true, ResultCode.SUCCESS);
        }

        public ApiResponse<List<Device>> GetAll()
        {            
            logger
                .Debug("Getting all devices");
            return new ApiResponse<List<Device>>("Success", deviceRepository.GetAll() ?? new List<Device>(), ResultCode.SUCCESS);
        }

        public  ApiResponse<List<Device>> Search(DeviceSearchRequest searchRequest)
        {
            logger
                .ForContext("DeviceId", searchRequest.DeviceId ?? Guid.Empty)
                .ForContext("IpAddress", searchRequest.IpAddress)
                .Information("Searching for device");
            var results = deviceRepository.Search(searchRequest.DeviceId, searchRequest.IpAddress, searchRequest.DeviceName.ToDeviceKey());

            if(results.Count() > 0)
            {
                return new ApiResponse<List<Device>>( "Success", results, ResultCode.SUCCESS);
            }
            return new ApiResponse<List<Device>>( "Not Found", results , ResultCode.NOTFOUND);
        }

        public ApiResponse<bool> Delete(string deviceName)
        {
            deviceRepository.Delete(deviceName.ToDeviceKey());
            return new ApiResponse<bool>("Success", true, ResultCode.SUCCESS);
        }
    }
}