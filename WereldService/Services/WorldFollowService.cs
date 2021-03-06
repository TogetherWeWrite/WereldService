﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WereldService.Entities;
using WereldService.Exceptions;
using WereldService.Helpers;
using WereldService.Repositories;

namespace WereldService.Services
{
    public class WorldFollowService : IWorldFollowService
    {
        private readonly IUserRepository _userRepository;
        private readonly IWorldRepository _worldRepository;
        private readonly IAuthenticationHelper _authenticationHelper;
        public WorldFollowService(IUserRepository userRepository, IWorldRepository worldRepository, IAuthenticationHelper authenticationHelper)
        {
            this._userRepository = userRepository;
            this._worldRepository = worldRepository;
            this._authenticationHelper = authenticationHelper;
        }

        public async Task<bool> FollowWorld(Guid userId, Guid worldId, string jwt)
        {
            var user = await getUser(userId);
            var world = await GetWorld(worldId);
            if (user.Id == _authenticationHelper.getUserIdFromToken(jwt))
            {
                if (user.WorldFollowed == null)
                {
                    user.WorldFollowed = new List<Guid>();
                }
                if (user.WorldFollowed.Contains(worldId))
                {
                    throw new WorldAlreadyFollowedException("The user: " + user.Name + " already follows the world: " + world.Title + ".");
                }
                else
                {
                    user.WorldFollowed.Add(world.Id);
                    await _userRepository.Update(userId, user);
                    world.Followers.Add(user);
                    await _worldRepository.Update(world.Id, world);
                    return true;
                }
            }
            else
            {
                throw new NotAuthorisedException("You are not authorised to add this user as follower");
            }
        }

        public async Task<bool> UnFollowWorld(Guid userId, Guid worldId, string jwt)
        {
            var user = await getUser(userId);
            var world = await GetWorld(worldId);
            if (user.Id == _authenticationHelper.getUserIdFromToken(jwt))
            {
                if (user.WorldFollowed == null)
                {
                    user.WorldFollowed = new List<Guid>();
                }
                if (!user.WorldFollowed.Contains(worldId))
                {
                    throw new UserDoesNotFollowWorldException("The user: " + user.Name + " already does not follow the world: " + world.Title + ". So he can not unfollow this world.");
                }
                else
                {
                    user.WorldFollowed.RemoveAt(user.WorldFollowed.FindIndex(id => id == worldId));
                    await _userRepository.Update(userId, user);
                    world.Followers.RemoveAt(world.Followers.FindIndex(x => x.Id == user.Id));
                    await _worldRepository.Update(world.Id, world);
                    return true;
                }
            }
            else
            {
                throw new NotAuthorisedException("You are not authorised to add to unfollower this user as follower");
            }
        }

        /// <summary>
        /// Gets the user by id.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>User</returns>
        /// <exception cref="UserNotFoundException"></exception>
        private async Task<User> getUser(Guid userId)
        {
            return await _userRepository.Get(userId)
                ?? throw new UserNotFoundException("The user with the id: " + userId + " could not be found");
        }

        /// <summary>
        /// Get worlds by world id
        /// </summary>
        /// <param name="worldId"></param>
        /// <returns></returns>
        /// <exception cref="WorldNotFoundException">When world not found</exception>
        private async Task<World> GetWorld(Guid worldId)
        {
            return await _worldRepository.Get(worldId)
                ?? throw new WorldNotFoundException("The world with the Id: " + worldId + " Could not be found");
        }
    }
}
