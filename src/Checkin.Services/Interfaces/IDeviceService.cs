using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Checkin.Models;
using Checkin.Models.Dto;

namespace Checkin.Services.Interfaces
{
    public interface IDeviceService
    {
        ApiResponse<List<DeviceDto>> GetAll();
        ApiResponse<List<Device>> Search(SearchDto searchDto);
        ApiResponse<bool> Delete(string deviceName);
        ApiResponse<bool> CreateOrUpdate(Device device);
    }
}