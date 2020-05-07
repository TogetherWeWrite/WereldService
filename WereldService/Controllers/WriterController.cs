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
    [Route("follow")]
    [ApiController]
    public class WriterController : ControllerBase
    {
        private readonly IWorldFollowService _worldFollowService;
        public WriterController(IWorldFollowService worldFollowService)
        {
            this._worldFollowService = worldFollowService;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Post(WorldFollowModel request)
        {
            try
            {
                if (request.Follow)
                {
                    return Ok(await _worldFollowService.FollowWorld(request.UserId, request.WorldId));
                }
                else
                {
                    return Ok(await _worldFollowService.UnFollowWorld(request.UserId, request.WorldId));
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}