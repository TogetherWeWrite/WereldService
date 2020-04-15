using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WereldService.Entities;
using WereldService.Exceptions;
using WereldService.Models;
using WereldService.Repositories;

namespace WereldService.Services
{
    public class WorldManagementService : IWorldManagementService
    {
        private readonly IWorldRepository _worldRepository;
        public WorldManagementService(IWorldRepository WorldRepository)
        {
            this._worldRepository = WorldRepository;
        }

        public bool CreateWorld(WorldRequest request)
        {
            var world = new World()
            {
                Id = new Guid(),
                OwnerId = request.UserId,
                Title = request.Title
            };
            return _worldRepository.Create(world) != null;
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
    }
}
