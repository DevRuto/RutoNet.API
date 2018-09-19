using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RutoNet.API.Interfaces.Repository;
using RutoNet.API.Models.Gokz;
using Serilog;
using SqlKata;
using SqlKata.Execution;

namespace RutoNet.API.Repository
{
    public class GokzRepository : IGokzRepository
    {
        private readonly QueryFactory _db;

        private ILogger Logger => Log.ForContext<GokzRepository>();

        public GokzRepository(QueryFactory db)
        {
            _db = db;
        }

        public Task<IEnumerable<Map>> GetMapsByName(string name)
        {
            return _db.Query("maps").WhereContains("name", name).GetAsync<Map>();
        }

        public Task<IEnumerable<Time>> GetRecentTimes(int? limit = null)
        {
            var query = _db.Query("Times").OrderByDesc("Created");
            if (limit.HasValue)
                query = query.Limit(limit.Value);
            return query.GetAsync<Time>();
        }
    }
}