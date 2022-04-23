using System.Collections.Generic;
using System.Threading.Tasks;
using Checkin.Models;

namespace Checkin.Services.Interfaces
{
    public interface IDeviceService
    {
        List<Device> GetAll();
        List<Device> Search(int? deviceId, string ipAddress);
        bool Update(Device device);
        bool Add(Device device);
        void Delete(int id);
    }
}