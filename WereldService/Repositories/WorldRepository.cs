using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WereldService.Entities;
using WereldService.Models;
using WereldService.WereldStoreDatabaseSettings.authenticationservice.DatastoreSettings;

namespace WereldService.Repositories
{
    public class WorldRepository : IWorldRepository
    {
        private readonly IMongoCollection<World> _worlds;
        public WorldRepository(IWereldstoreDatabaseSettings set)
        {
            var client = new MongoClient(set.ConnectionString);
            var database = client.GetDatabase(set.DatabaseName);

            _worlds = database.GetCollection<World>(set.UserCollectionName);
        }

        public async Task<World> Create(World world)
        {
            await _worlds.InsertOneAsync(world);
            return world;
        }

        public async Task<World> Get(Guid id)
        {
            return await _worlds.Find(world => world.Id == id).FirstOrDefaultAsync();
        }

        public async Task<World> Get(string Title)
        {
            return await _worlds.Find(world => world.Title == Title).FirstOrDefaultAsync();
        }

        public async Task remove(Guid id)
        {
            await _worlds.DeleteOneAsync(world => world.Id == id);
        }

        public async Task<List<World>> Search(string searchWord)
        {
            return await _worlds.Find(world => world.Title.Contains(searchWord)).ToListAsync();
        }

        public async Task Update(Guid id, World update)
        {
            await _worlds.ReplaceOneAsync(world => world.Id == id, update);
        }

        public async Task<List<World>> GetWorldsFromUser(Guid userId)
        {
            return await _worlds.Find(world => world.Owner.Id == userId | world.Writers.Any(writer => writer.Id == userId)).ToListAsync();
        }

        public async Task<List<World>> GetWorlds(List<Guid> ids)
        {
            return await _worlds.Find(world => ids.Contains(world.Id)).ToListAsync();
        }
        
        public async Task<List<World>> GetMostPopularWorlds(int page)
        {
            int pagesize = 25;
            var filter = Builders<World>.Filter.Exists(world => world.Followers, true);
            var worlds = await _worlds.Find(filter)
                .Sort(Builders<World>.Sort.Descending(x => x.Followers))
                .Skip((page - 1) * pagesize)
                .Limit(pagesize)
                .ToListAsync();
            return worlds;
        }

    }
}
