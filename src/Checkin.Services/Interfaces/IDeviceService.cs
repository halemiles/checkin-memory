using System.Collections.Generic;
using System.Threading.Tasks;
using Checkin.Models;

namespace Checkin.Services.Interfaces
{
    public interface IDeviceService
    {
        List<Device> GetAll();
        Device GetByKey(string key);
        List<Device> Search(int? deviceId, string ipAddress);
        void Delete(int id);
        bool CreateOrUpdate(Device device);
    }
}