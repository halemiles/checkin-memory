using System.Collections.Generic;
using System.Threading.Tasks;
using Checkin.Models;

namespace Checkin.Services.Interfaces
{
    public interface IDeviceService
    {
        List<Device> GetAll();
        Device GetByDeviceName(string deviceName);
        List<Device> Search(int? deviceId, string ipAddress);
        void DeleteByDeviceName(string deviceName);
        bool CreateOrUpdate(Device device);
    }
}