using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WereldService.Models;

namespace WereldService.Services
{
    public interface IWorldOverviewService
    {
        /// <summary>
        /// Get Single World with details on ID
        /// </summary>
        /// <param name="id">Guid of World</param>
        /// <returns>Title and guid</returns>
        Task<WorldWithDetails> GetWorld(Guid id);
        /// <summary>
        /// Will return the search of a world can be null which means no worlds found.
        /// </summary>
        /// <param name="search">Title contains the search.</param>
        /// <returns>0, 1 or more Overviewworlds depening how many exists</returns>
        Task<List<WorldOverviewModel>> Search(string search);

        /// <summary>
        /// Gets the worlds which are relevant to this user.
        /// </summary>
        /// <param name="userid">the id of the user</param>
        /// <returns></returns>
        Task<List<WorldWithDetails>> GetWorldsForUser(int userid);


        /// <summary>
        /// Method used for getting multiple worlds, with a list of guids.
        /// </summary>
        /// <param name="ids">ids of the worlds you want to get.</param>
        /// <returns></returns>
        Task<List<WorldWithDetails>> GetWorlds(List<Guid> ids);

    }
}
