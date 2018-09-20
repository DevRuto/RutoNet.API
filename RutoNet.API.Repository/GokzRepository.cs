using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
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

        public Task<IEnumerable<Time>> GetRecentTimes(int? limit)
        {
            var query = _db.Query("times")
                .Join("players", "players.steamid32", "times.steamid32")
                .Join("mapcourses", "mapcourses.mapcourseid", "times.mapcourseid")
                .Join("maps", "maps.mapid", "mapcourses.mapid")
                .OrderByDesc("times.created");

            if (limit.HasValue)
                query = query.Limit(limit.Value);

            var compiled = _db.Compiler.Compile(query);
            Logger.Debug("RAW SQL: " + compiled.RawSql);

            return _db.Connection.QueryAsync<Time, Player, MapCourse, Map, Time>(compiled.Sql,
                (time, player, mapcourse, map) =>
                {
                    time.Player = player;
                    time.Course = mapcourse;
                    mapcourse.Map = map;
                    return time;
                }, new { p0 = limit.Value }, splitOn: "steamid32,mapcourseid,mapid");
        }
    }
}