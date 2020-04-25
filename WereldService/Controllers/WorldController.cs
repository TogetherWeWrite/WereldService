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
    [Route("worldmanagement")]
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
                var world = _worldManagementService.CreateWorld(request);
                return Ok(world);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

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

        [HttpDelete]
        public ActionResult Delete(WorldDeleteRequest request)
        {
            try
            {
                _worldManagementService.DeleteWorld(request);
                return Ok("world: " + request.Title + "succesfully deleted");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}