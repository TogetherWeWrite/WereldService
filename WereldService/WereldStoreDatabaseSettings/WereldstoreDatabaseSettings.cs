using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WereldService.WereldStoreDatabaseSettings
{
    namespace authenticationservice.DatastoreSettings
    {
        public class WereldstoreDatabaseSettings : IWereldstoreDatabaseSettings
        {
            public string UserCollectionName { get; set; }
            public string WorldCollectionName { get; set; }
            public string ConnectionString { get; set; }
            public string DatabaseName { get; set; }
        }

        public interface IWereldstoreDatabaseSettings
        {
            string UserCollectionName { get; set; }
            string WorldCollectionName { get; set; }
            string ConnectionString { get; set; }
            string DatabaseName { get; set; }
        }
    }
}
