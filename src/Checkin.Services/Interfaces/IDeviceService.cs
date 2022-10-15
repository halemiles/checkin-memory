using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Checkin.Models;

namespace Checkin.Services.Interfaces
{
    public interface IDeviceService
    {
        List<Device> GetAll();
        Device GetByKey(string key);
        List<Device> Search(Guid? deviceId, string ipAddress);
        void Delete(string deviceName);
        bool CreateOrUpdate(Device device);
    }
}