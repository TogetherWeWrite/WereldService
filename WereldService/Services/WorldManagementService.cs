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
        private readonly IUserRepository __userRepository;
        private readonly IUserHelper _userHelper;
        public WorldManagementService(IWorldRepository WorldRepository, IUserRepository userRepository, IUserHelper userHelper)
        {
            this._worldRepository = WorldRepository;
            this.__userRepository = userRepository;
            this._userHelper = userHelper;
        }

        public async Task<WorldOverviewModel> CreateWorld(WorldRequest request)
        {
            var world = new World()
            {
                Id = new Guid(),
                OwnerId = request.UserId,
                Title = request.Title
            };
            //owner gets from repository and if there is no owner in this datastore then update it from the authenticationservice.
            //TODO: Make the user automatically update with rabbitmq.
            var owner = await __userRepository.Get(world.OwnerId) ?? await UpdateUser(world.OwnerId);
            world = await _worldRepository.Create(world);
            return new WorldOverviewModel { Title = world.Title, WorldId = world.Id, OwnerId = owner.Id, OwnerName = owner.Name};
        }

        public bool DeleteWorld(WorldDeleteRequest request)
        {
            var world = _worldRepository.Get(request.WorldId).Result;
            if (world.Title == request.Title && world.OwnerId == request.UserId)
            {
                _worldRepository.remove(request.WorldId);
                return true;
            }
            else
            {
                throw new VariablesDoNotMatchException("The title or/and ownerId are not correct, so this world will not be deleted");
            }
        }

        public bool UpdateWorld(WorldUpdateRequest request)
        {
            if(_worldRepository.Get(request.WorldId).Result != null)
            {
                var world = new World()
                {
                    Id = request.WorldId,
                    OwnerId = request.UserId,
                    Title = request.Title
                };
                _worldRepository.Update(request.WorldId, world);
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
        private async Task<User> UpdateUser(int userId)
        {
            try
            {
                return await _userHelper.GetOwnerFromAuthentication(userId);
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
    }
}
