using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RutoNet.API.Interfaces.Repository;
using SqlKata;
using SqlKata.Execution;

namespace RutoNet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GokzController : Controller
    {
        private IGokzRepository _gokz;
        private QueryFactory _db;

        public GokzController(IGokzRepository repo, QueryFactory db)
        {
            _gokz = repo;
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetMap(string map)
        {
            return Json(await _gokz.GetMapsByName(map));
        }
    }
}