using System.Threading.Tasks;
using WereldService.Models;
using WereldService.Exceptions;

namespace WereldService.Services
{
    public interface IWorldManagementService : IWorldUserManagementService
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
        /// <exception cref="VariablesDoNotMatchException"></exception>
        Task<bool> DeleteWorld(WorldDeleteRequest request);
        /// <summary>
        /// Will only change the title of a world
        /// </summary>
        /// <param name="request">hold the id of the owner and the guid of the world which it will use to validate if the user is indeed the person that he says he is. and to update title</param>
        /// <returns>if request has been recieved</returns>
        /// <exception cref="UserNotOwnerException"></exception>
        Task<bool> UpdateWorld(WorldUpdateRequest request);
    }

    public interface IWorldUserManagementService
    {
        /// <summary>
        /// Adds user to the writer array of world. with the id and the name.
        /// </summary>
        /// <param name="writerWorld">Holde the world id and the userid</param>
        /// <returns>bool if succesful</returns>
        /// <exception cref="WorldNotFoundException"></exception>
        /// <exception cref="UserIsAlreadyAWriterException"></exception>
        Task<bool> AddWriterToWorld(WriterWorld writerWorld);
        /// <summary>
        /// Deletes a writer from the world.
        /// </summary>
        /// <param name="writerWorld"></param>
        /// <returns></returns>
        /// <exception cref="WorldNotFoundException"></exception>
        /// <exception cref="WriterDoesNotExistInWorldException"></exception>
        Task<bool> DeleteWriterFromWorld(WriterWorld writerWorld);
    }
}
