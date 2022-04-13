using System.Collections.Generic;
using System.Threading.Tasks;
using Checkin.Models;

namespace Checkin.Services.Interfaces
{
    public interface IDeviceService //TODO - Make this generic
    {
        Task<List<Device>> GetAll();
        void Update(Device device);
        Device GetByIp(string ipAddress);
        Device GetByDevice(int deviceId);
        Task<bool> Add(Device device);
        void Delete(int id);
    }
}