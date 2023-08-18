using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Checkin.Models;

namespace Checkin.Services.Interfaces
{
    public interface IDeviceService
    {
        ApiResponse<List<Device>> GetAll();
        ApiResponse<List<Device>> Search(DeviceSearchRequest searchRequest);
        ApiResponse<bool> Delete(string deviceName);
        Task<ApiResponse<bool>> CreateOrUpdate(Device device);
    }
}