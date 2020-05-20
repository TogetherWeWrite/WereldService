using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WereldService.Exceptions;

namespace WereldService.Services
{
    public interface IWorldFollowService
    {
        /// <summary>
        /// Used for Following A World. will return true if succesfull
        /// </summary>
        /// <param name="userId">The id of the user</param>
        /// <param name="worldId">The Id of the world</param>
        /// <returns></returns>
        /// <exception cref="WorldNotFoundException"></exception>
        /// <exception cref="UserNotFoundException"></exception>
        /// <exception cref="WorldAlreadyFollowedException"></exception>
        Task<bool> FollowWorld(Guid userId, Guid worldId);

        /// <summary>
        /// Used for Unfollowing A World. will return true if succesfull
        /// </summary>
        /// <param name="userId">The id of the user</param>
        /// <param name="worldId">The Id of the world</param>
        /// <returns></returns>
        /// <exception cref="WorldNotFoundException"></exception>
        /// <exception cref="UserNotFoundException"></exception>
        /// <exception cref="UserDoesNotFollowWorldException"></exception>
        Task<bool> UnFollowWorld(Guid userId, Guid worldId);
    }
}
