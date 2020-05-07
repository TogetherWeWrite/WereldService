using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using WereldService.Entities;
using WereldService.Exceptions;
using WereldService.Models;
using WereldService.Repositories;

namespace WereldService.Services
{
    public class WorldOverviewService : IWorldOverviewService
    {
        private readonly IWorldRepository _worldRepository;
        public WorldOverviewService(IWorldRepository worldRepository)
        {
            this._worldRepository = worldRepository;
        }

        public async Task<WorldWithDetails> GetWorld(Guid id)
        {
            var world = await _worldRepository.Get(id);
            if (world == null)
            {
                throw new WorldNotFoundException("World with ID: " + id + " Could not be found");
            }
            else
            {
                return world.ToDetailWorldModel();
            }
        }

        public async Task<List<WorldWithDetails>> GetWorlds(int userid)
        {
            var worlds = await _worldRepository.GetWorldsFromUser(userid);
            return worlds.ToWorldWithDetailsList();
        }

        public async Task<List<WorldOverviewModel>> Search(string search)
        {
            var worlds = await _worldRepository.Search(search);
            return worlds.ToWorldOverviewModelList();
        }

        public async Task<List<WorldWithDetails>> GetWorlds(List<Guid> ids)
        {
            var worlds = await _worldRepository.GetWorlds(ids);
            return worlds.ToWorldWithDetailsList();
        }

    }
}
