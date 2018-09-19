using System.Collections.Generic;
using System.Threading.Tasks;
using RutoNet.API.Models.Gokz;

namespace RutoNet.API.Interfaces.Repository
{
    public interface IGokzRepository
    {
        Task<IEnumerable<Map>> GetMapsByName(string name);

        Task<IEnumerable<Time>> GetRecentTimes(int? limit = null);
    }
}