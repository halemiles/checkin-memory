using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Checkin.Models;

namespace Checkin.Services.Interfaces
{
    public interface IDeviceService
    {
        List<Device> GetAll();
        List<Device> Search(Guid? deviceId, string ipAddress, string name);
        void Delete(string deviceName);
        bool CreateOrUpdate(Device device);
    }
}