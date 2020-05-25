using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WereldService.Exceptions;
using WereldService.Models;
using WereldService.Services;

namespace WereldService.Controllers
{
    [Route("world/writer")]
    [ApiController]
    public class WorldWriterController : ControllerBase
    {

        private readonly IWorldUserManagementService _worldUserManagementService;
        private readonly IWorldOverviewService _worldOverviewService;

        public WorldWriterController(IWorldUserManagementService worldUserManagementService, IWorldOverviewService worldOverviewService)
        {
            this._worldUserManagementService = worldUserManagementService;
            this._worldOverviewService = worldOverviewService;
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddWriterToWorld(WriterWorld addRequest, [FromHeader(Name = "Authorization")] string jwt)
        {
            try
            {
                return Ok(_worldUserManagementService.AddWriterToWorld(addRequest, jwt).Result);
            }
            catch (UserIsAlreadyAWriterException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(NotAuthorisedException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Authorize]
        public ActionResult RemoveWriterFromWorld(WriterWorld removerequest, [FromHeader(Name = "Authorization")] string jwt)
        {
            try
            {
                return Ok(_worldUserManagementService.DeleteWriterFromWorld(removerequest, jwt).Result);
            }
            catch (NotAuthorisedException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)//TODO specific exception handling
            {
                return BadRequest(ex.Message);
            }


        }
    }
}