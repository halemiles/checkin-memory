using System.Collections.Generic;
using System.Threading.Tasks;
using Checkin.Models;

namespace Checkin.Repositories
{
    public interface IDistrbutedCacheRepository
    {
        Task<List<Device>> GetAll();
        Task<bool> Set(List<Device> devices);
    }
}