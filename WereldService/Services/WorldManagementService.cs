﻿using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WereldService.Entities;
using WereldService.Exceptions;
using WereldService.Helpers;
using WereldService.Models;
using WereldService.Repositories;

namespace WereldService.Services
{
    public class WorldManagementService : IWorldManagementService
    {
        private readonly IWorldRepository _worldRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserHelper _userHelper;
        public WorldManagementService(IWorldRepository WorldRepository, IUserRepository userRepository, IUserHelper userHelper)
        {
            this._worldRepository = WorldRepository;
            this._userRepository = userRepository;
            this._userHelper = userHelper;
        }

        

        public async Task<WorldOverviewModel> CreateWorld(WorldRequest request)
        {
            var world = new World()
            {
                Id = new Guid(),
                Title = request.Title,
                Writers = new List<User>(),
                Followers = new List<User>()
            };
            //owner gets from repository and if there is no owner in this datastore then update it from the authenticationservice.
            //TODO: Make the user automatically update with rabbitmq.
            world.Owner = await GetUser(request.UserId);
            world = await _worldRepository.Create(world);
            return new WorldOverviewModel { Title = world.Title, WorldId = world.Id, OwnerId = world.Owner.Id, OwnerName = world.Owner.Name};
        }

        public async Task<bool> DeleteWorld(WorldDeleteRequest request)
        {
            var world = _worldRepository.Get(request.WorldId).Result;
            if (world.Title == request.Title && world.Owner.Id == request.UserId)
            {
                await _worldRepository.remove(request.WorldId);
                return true;
            }
            else
            {
                throw new VariablesDoNotMatchException("The title or/and ownerId are not correct, so this world will not be deleted");
            }
        }

        public async Task<bool> AddWriterToWorld(WriterWorld writerWorld)
        {
            //Step 1: get user Entity from writer id
            var user = await GetUser(writerWorld.WriterId);
            //Step 2: get world Entity from world id
            World world = await _worldRepository.Get(writerWorld.WorldId);
            if(world == null)
            {
                throw new WorldNotFoundException("The world with the id: " + writerWorld.WorldId + " Does not exist");
            }
            //step 3: If world has user already as a writer throw exception
            foreach(User writer in world.Writers)
            {
                if(writer.Id == user.Id)
                {
                    throw new UserIsAlreadyAWriterException("The user: " + user.Name + " Already is a writer in this world");
                }
            }
            //Step 3: update world
            world.AddWriter(user);
            await _worldRepository.Update(world.Id, world);
            return true;
        }

        public async Task<bool> DeleteWriterFromWorld(WriterWorld writerWorld)
        {
            //step 1: Get world
            World world = await _worldRepository.Get(writerWorld.WorldId);
            if (world == null)
            {
                throw new WorldNotFoundException("The world with the id: " + writerWorld.WorldId + " Does not exist");
            }
            //step 2: check if writer is indeed a writer on this world
            User writerInWorld = null;
            foreach (User writer in world.Writers)
            {
                if (writer.Id == writerWorld.WriterId)
                {
                    writerInWorld = writer;
                    
                    break;
                }
            }
            if (writerInWorld == null)
            {
                throw new WriterDoesNotExistInWorldException("Writer does not exist in world.");
            }
            //step 3 remove writer
            world.Writers.Remove(writerInWorld);
            await _worldRepository.Update(world.Id, world);
            return true;
        }

        public async Task<bool> UpdateWorld(WorldUpdateRequest request)
        {
            var world = await _worldRepository.Get(request.WorldId);
            if (world != null)
            {
                world.Title = request.Title;
                world.Owner = await _userRepository.Get(request.UserId) ?? await UpdateUser(request.UserId);
                try
                {
                    await _worldRepository.Update(request.WorldId, world);
                }
                catch(Exception ex)
                {
                    //TODO implement right exceptions
                    return false;
                }
                return true;
            }
            else
            {
                throw new WorldNotFoundException("The world with the id: " + request.WorldId + " does not exist");
            }
        }
        /// <summary>
        /// Updates user from the authentication repository with the UserHelper function <see cref="UserHelper.GetOwnerFromAuthentication(int)"/>
        /// If this doesn't work it will fill the user name with Unknown
        /// </summary>
        /// <param name="userId">UserID</param>
        /// <returns>User</returns>
        private async Task<User> UpdateUser(Guid userId)
        {
            try
            {
                var user = await _userHelper.GetOwnerFromAuthentication(userId);
                user.WorldFollowed = new List<Guid>();
                return await _userRepository.Create(user);
            }
            catch(UserDoesNotExistInAuthenticationServiceException ex)
            {
                return new User() { Id=userId, Name="Unknown"};
            }
            catch(AuthenticationServiceIsNotOnlineException ex)
            {
                return new User() { Id=userId, Name="Unknown"};
            }
            
        }
        private async Task<User> GetUser(Guid userId)
        {
            return await _userRepository.Get(userId) ?? await UpdateUser(userId);
        }
    }
}
