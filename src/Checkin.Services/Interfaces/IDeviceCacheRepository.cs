using System.Collections.Generic;
using Checkin.Models;

namespace Checkin.Services.Interfaces
{
    public interface IDeviceCacheRepository
    {
        bool Set(List<Device> value);
        bool Set(string key, Device value); //TODO - Remove key and allow the device ID to do the work
        Device GetByKey(string key);
        List<Device> GetAll();
        List<Device> Search(int? deviceId, string ipAddress);
    }
}