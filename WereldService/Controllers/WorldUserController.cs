using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WereldService.Exceptions;
using WereldService.Models;
using WereldService.Services;

namespace WereldService.Controllers
{
    [Route("world/user")]
    [ApiController]
    public class WorldUserController : ControllerBase
    {
        private readonly IWorldUserManagementService _worldUserManagementService;
        private readonly IWorldOverviewService _worldOverviewService;

        public WorldUserController(IWorldUserManagementService worldUserManagementService, IWorldOverviewService worldOverviewService)
        {
            this._worldUserManagementService = worldUserManagementService;
            this._worldOverviewService = worldOverviewService;
        }


        [HttpGet]
        public ActionResult<List<WorldWithDetails>> GetWorldsOfWhichUserIsPart(int userId)
        {
            try
            {
                return Ok(_worldOverviewService.GetWorlds(userId).Result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}