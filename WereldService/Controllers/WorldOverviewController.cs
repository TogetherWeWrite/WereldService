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
    [Route("worldoverview")]
    [ApiController]
    public class WorldOverviewController : ControllerBase
    {
        private readonly IWorldOverviewService _worldOverviewService;
        public WorldOverviewController(IWorldOverviewService worldOverviewService)
        {
            this._worldOverviewService = worldOverviewService;
        }

        [Route("search")]
        [HttpGet]
        public List<WorldOverviewModel> Get(WorldRequest request)
        {
            return _worldOverviewService.Search(request.Title);
        }
        
        [HttpGet]
        public WorldOverviewModel get(WorldUpdateRequest request)
        {
            return _worldOverviewService.GetWorld(request.WorldId);
        }
    }
}