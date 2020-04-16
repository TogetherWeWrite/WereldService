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
        /// Get Single World on ID
        /// </summary>
        /// <param name="id">Guid of World</param>
        /// <returns>Title and guid</returns>
        WorldOverviewModel GetWorld(Guid id);
        /// <summary>
        /// Will return the search of a world can be null which means no worlds found.
        /// </summary>
        /// <param name="search">Title contains the search.</param>
        /// <returns>0, 1 or more Overviewworlds depening how many exists</returns>
        List<WorldOverviewModel> Search(string search);

    }
}
