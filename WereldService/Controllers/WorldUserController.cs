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

        //TODO: Authorisation that only the worldowner can do this.
        [HttpPost]
        public ActionResult AddWriterToWorld(WriterWorld addRequest)
        {
            try
            {
                return Ok(_worldUserManagementService.AddWriterToWorld(addRequest).Result);
            }
            catch (UserIsAlreadyAWriterException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)//TODO  specific exception handling
            {
                return BadRequest(ex.Message);
            }
        }

        //TODO: Authorisation that only the worldowner can do this.
        [HttpDelete]
        public ActionResult RemoveWriterFromWorld(WriterWorld removerequest)
        {
            try
            {
                return Ok(_worldUserManagementService.DeleteWriterFromWorld(removerequest).Result);
            }
            catch (Exception ex)//TODO specific exception handling
            {
                return BadRequest(ex.Message);
            }


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