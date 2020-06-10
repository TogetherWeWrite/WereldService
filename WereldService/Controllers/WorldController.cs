using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using WereldService.Exceptions;
using WereldService.Helpers;
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
        private readonly IAuthenticationHelper _authenticationHelper;
        public WorldController(IWorldManagementService worldManagementService, IWorldOverviewService worldOverviewService, IAuthenticationHelper authenticationHelper)
        {
            this._worldManagementService = worldManagementService;
            this._worldOverviewService = worldOverviewService;
            this._authenticationHelper = authenticationHelper;
        }



        /// <summary>
        /// Create world
        /// </summary>
        /// <param name="request"><see cref="WorldRequest"/></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public ActionResult<WorldOverviewModel> Post(WorldRequest request, [FromHeader(Name = "Authorization")] string jwt)
        {
            var idclaim = _authenticationHelper.getUserIdFromToken(jwt);
            if (idclaim == request.UserId)
            {
                try
                {
                    var world = _worldManagementService.CreateWorld(request).Result;
                    return Ok(world);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
            }
            else
            {
                return Unauthorized("You are not authorised to do this");
            }
        }

        /// <summary>
        /// Updates world
        /// </summary>
        /// <param name="updateRequest"><see cref="WorldUpdateRequest"/></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        public ActionResult Put(WorldUpdateRequest updateRequest, [FromHeader(Name = "Authorization")] string jwt)
        {
            var idclaim = _authenticationHelper.getUserIdFromToken(jwt);
            if (idclaim == updateRequest.UserId)
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
            else
            {
                return Unauthorized("You are not authorised to do this");
            }
        }

        /// <summary>
        /// Deletes world
        /// </summary>
        /// <param name="request"><see cref="WorldDeleteRequest"/></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        public async Task<ActionResult> Delete(WorldDeleteRequest request, [FromHeader(Name = "Authorization")] string jwt)
        {
            var idclaim = _authenticationHelper.getUserIdFromToken(jwt);
            if (idclaim == request.UserId)
            {
                try
                {
                    if (await _worldManagementService.DeleteWorld(request))
                    {
                        return Ok("world: " + request.Title + "succesfully deleted");
                    }
                    else
                    {
                        return BadRequest("World not succesfully deleted");
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return Unauthorized("You are not authorised to do this");

            }
        }

        /// <summary>
        /// Gets the details of a world, need world id
        /// </summary>
        /// <param name="id">Guid in the form of a string</param>
        /// <returns><see cref="WorldWithDetails"/></returns>
        [HttpGet]
        public async Task<ActionResult<WorldWithDetails>> Get(Guid id)
        {
            try
            {
                return Ok(await _worldOverviewService.GetWorld(id));
            }
            catch (WorldNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("popularlist")]
        public async Task<ActionResult<WorldWithDetailsAndFollowers>> GetMostPopularWorlds(int page)
        {

            try
            {
                List<WorldWithDetailsAndFollowers> worlds = await _worldOverviewService.GetMostPopularWorlds(page);
                return Ok(worlds);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}