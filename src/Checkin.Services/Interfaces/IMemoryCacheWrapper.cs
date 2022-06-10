using System.Collections.Generic;
using Checkin.Models;

namespace Checkin.Services.Interfaces
{
    public interface IMemoryCacheWrapper
    {
        void TryGetValue(string cacheName, out List<Device> devices);
    }
}