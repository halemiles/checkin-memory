using System.Collections.Generic;
using System.Threading.Tasks;
using Checkin.Models;

namespace Checkin.Services.Interfaces
{
    public interface IDeviceService //TODO - Make this generic
    {
        List<Device> GetAll();
        Device GetByIp(string ipAddress);
        Device GetByDevice(int deviceId);
        void Add(Device device);
        
    }
}