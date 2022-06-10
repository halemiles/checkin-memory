using System.Collections.Generic;
using Checkin.Models;
using Checkin.Services.Interfaces;

namespace Checkin.Repositories
{
    public class MemoryCacheWrapper : IMemoryCacheWrapper

    {
        public void TryGetValue(string cacheName, out List<Device> devices)
        {
            throw new System.NotImplementedException();
        }
    }
}