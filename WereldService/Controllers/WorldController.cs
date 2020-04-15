using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WereldService.Models;
using WereldService.Services;

namespace WereldService.Controllers
{
    [Route("world")]
    [ApiController]
    public class WorldController : ControllerBase
    {
        private readonly IWorldManagementService _worldManagementService;
        public WorldController(IWorldManagementService worldManagementService)
        {
            this._worldManagementService = worldManagementService;
        }
        [HttpPost]
        public ActionResult Post(WorldRequest request)
        {
            try
            {
                _worldManagementService.CreateWorld(request);
                return Ok("World succesfully created");
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}