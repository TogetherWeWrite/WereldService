using System.Threading.Tasks;
using WereldService.Models;

namespace WereldService.Services
{
    public interface IWorldManagementService
    {
        /// <summary>
        /// Create a world
        /// </summary>
        /// <param name="request">The user id which will be the owner and the title that you want to name the world</param>
        /// <returns></returns>
        Task<WorldOverviewModel>CreateWorld(WorldRequest request);
        /// <summary>
        /// Deletes world
        /// </summary>
        /// <param name="request">All params should be correct if not the world will not be deleted</param>
        /// <returns>returns true if world is succesfully deleted</returns>
        bool DeleteWorld(WorldDeleteRequest request);
        /// <summary>
        /// Will only change the title of a world
        /// </summary>
        /// <param name="request">hold the id of the owner and the guid of the world which it will use to validate if the user is indeed the person that he says he is. and to update title</param>
        /// <returns>if request has been recieved</returns>
        bool UpdateWorld(WorldUpdateRequest request);
    }
}
