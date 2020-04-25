using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using WereldService.Entities;
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

        public WorldOverviewModel GetWorld(Guid id)
        {
            return _worldRepository.Get(id).Result.ToWorldOverviewModel();
        }


        public List<WorldOverviewModel> Search(string search)
        {
            return _worldRepository.Search(search).Result.ToWorldOverviewModelList();
        }
    }
}
