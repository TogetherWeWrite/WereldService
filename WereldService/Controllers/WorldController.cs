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
    [Route("world")]
    [ApiController]
    public class WorldController : ControllerBase
    {
        private readonly IWorldManagementService _worldManagementService;
        private readonly IWorldOverviewService _worldOverviewService;
        public WorldController(IWorldManagementService worldManagementService, IWorldOverviewService worldOverviewService)
        {
            this._worldManagementService = worldManagementService;
            this._worldOverviewService = worldOverviewService;
        }



        /// <summary>
        /// Create world
        /// </summary>
        /// <param name="request"><see cref="WorldRequest"/></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<WorldOverviewModel> Post(WorldRequest request)
        {
            try
            {
                var world = _worldManagementService.CreateWorld(request).Result;
                return Ok(world);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// Updates world
        /// </summary>
        /// <param name="updateRequest"><see cref="WorldUpdateRequest"/></param>
        /// <returns></returns>
        [HttpPut]
        public ActionResult Put(WorldUpdateRequest updateRequest)
        {
            try
            {
                _worldManagementService.UpdateWorld(updateRequest);
                return Ok("Worldname changed to:" + updateRequest.Title);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes world
        /// </summary>
        /// <param name="request"><see cref="WorldDeleteRequest"/></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult> Delete(WorldDeleteRequest request)
        {
            try
            {
                if(await _worldManagementService.DeleteWorld(request))
                {
                    return Ok("world: " + request.Title + "succesfully deleted");
                }
                else
                {
                    return BadRequest("World not succesfully deleted");
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Gets the details of a world, need world id
        /// </summary>
        /// <param name="id">Guid in the form of a string</param>
        /// <returns><see cref="WorldWithDetails"/></returns>
        [HttpGet]
        public ActionResult<WorldWithDetails> Get(Guid id)
        {
            try
            {
                return Ok(_worldOverviewService.GetWorld(id).Result);
            }
            catch(WorldNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("popularlist")]
        public async  Task<ActionResult<WorldWithDetailsAndFollowers>> GetMostPopularWorlds(int page)
        {
            try
            {
               List<WorldWithDetailsAndFollowers> worlds = await _worldOverviewService.GetMostPopularWorlds(page);
               return Ok(worlds);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}