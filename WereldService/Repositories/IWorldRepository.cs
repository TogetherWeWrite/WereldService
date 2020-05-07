using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WereldService.Entities;

namespace WereldService.Repositories
{
    public interface IWorldRepository
    {
        /// <summary>
        /// Will get Worlds whcih have the search title max of 25.
        /// </summary>
        /// <param name="searchWord">If title has this within its name than it will be included in the result</param>
        /// <returns>List of worlds which contain the searchWord.</returns>
        Task<List<World>> Search(string searchWord);
        Task<World> Get(Guid id);
        Task<World> Get(string Title);
        Task<World> Create(World world);
        Task Update(Guid id, World update);
        Task remove(Guid id);
        Task<List<World>> GetWorldsFromUser(int userId);
        Task<List<World>> GetWorlds(List<Guid> ids);

    }
}
