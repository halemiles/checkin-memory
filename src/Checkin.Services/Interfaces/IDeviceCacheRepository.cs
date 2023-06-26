using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Checkin.Models;

namespace Checkin.Services.Interfaces
{
    public interface IDeviceCacheRepository
    {
        bool Set(List<Device> value);
        bool Set(string key, Device value); //TODO - Remove key and allow the device ID to do the work
        Task<Device> GetByKey(string key);
        List<Device> GetAll();
        List<Device> Search(Guid? deviceId, string ipAddress, string name);
        void Delete(string deviceName);
    }
}