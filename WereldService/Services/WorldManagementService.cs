using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WereldService.Entities;
using WereldService.Exceptions;
using WereldService.Helpers;
using WereldService.Models;
using WereldService.Publishers;
using WereldService.Repositories;

namespace WereldService.Services
{
    public class WorldManagementService : IWorldManagementService
    {
        private readonly IWorldRepository _worldRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationHelper _authenticationHelper;
        private readonly IWorldPublisher _worldPublisher;
        public WorldManagementService(IWorldRepository WorldRepository, IUserRepository userRepository, IAuthenticationHelper authenticationHelper, IWorldPublisher worldPublisher)
        {
            this._worldRepository = WorldRepository;
            this._userRepository = userRepository;
            this._authenticationHelper = authenticationHelper;
            this._worldPublisher = worldPublisher;
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
            var user = await GetUser(request.UserId);
            if (user == null)
            {
                throw new UserNotFoundException("The user does not exist, if you account has been recently made please wait a minute before trying again.");
            }
            world.Owner = user;
            world = await _worldRepository.Create(world);
            await _worldPublisher.PublishNewWorld(world);
            return new WorldOverviewModel { Title = world.Title, WorldId = world.Id, OwnerId = world.Owner.Id, OwnerName = world.Owner.Name };
        }

        public async Task<bool> DeleteWorld(WorldDeleteRequest request)
        {
            var world = _worldRepository.Get(request.WorldId).Result;
            if (world.Title == request.Title && world.Owner.Id == request.UserId)
            {
                await _worldRepository.remove(request.WorldId);
                await _worldPublisher.DeleteWorldWorld(request.WorldId);
                return true;
            }
            else
            {
                throw new VariablesDoNotMatchException("The title or/and ownerId are not correct, so this world will not be deleted");
            }
        }

        public async Task<bool> AddWriterToWorld(WriterWorld writerWorld, string jwt)
        {
            //Step 1: get user Entity from writer id
            var user = await GetUser(writerWorld.WriterId);
            if (user == null)
            {
                throw new UserNotFoundException("This user does not exist, if this user has just made his account you need to wait a few minutes before trying again");
            }
            //Step 2: get world Entity from world id
            World world = await _worldRepository.Get(writerWorld.WorldId);
            if (world.Owner.Id == _authenticationHelper.getUserIdFromToken(jwt))//authorise
            {
                if (world == null)
                {
                    throw new WorldNotFoundException("The world with the id: " + writerWorld.WorldId + " Does not exist");
                }
                //step 3: If world has user already as a writer throw exception
                foreach (User writer in world.Writers)
                {
                    if (writer.Id == user.Id)
                    {
                        throw new UserIsAlreadyAWriterException("The user: " + user.Name + " Already is a writer in this world");
                    }
                }
                //Step 3: update world
                world.AddWriter(user);
                await _worldRepository.Update(world.Id, world);

                return true;
            }
            else
            {
                throw new NotAuthorisedException("You are not eligble to remove a writer to this world.");
            }
        }

        public async Task<bool> DeleteWriterFromWorld(WriterWorld writerWorld, string jwt)
        {
            //step 1: Get world
            World world = await _worldRepository.Get(writerWorld.WorldId);
            if (world.Owner.Id == _authenticationHelper.getUserIdFromToken(jwt))
            {
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
            else
            {
                throw new NotAuthorisedException("You are not eligble to remove a writer to this world.");
            }
        }

        public async Task<bool> UpdateWorld(WorldUpdateRequest request)
        {
            var world = await _worldRepository.Get(request.WorldId);
            if (world != null)
            {
                world.Title = request.Title;
                world.Owner = await _userRepository.Get(request.UserId);
                await _worldRepository.Update(request.WorldId, world);
                await _worldPublisher.PublishUpdateWorld(request.WorldId, world.Title);
                return true;
            }
            else
            {
                throw new WorldNotFoundException("The world with the id: " + request.WorldId + " does not exist");
            }
        }
        private async Task<User> GetUser(Guid userId)
        {
            return await _userRepository.Get(userId);
        }
    }
}
