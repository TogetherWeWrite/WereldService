using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WereldService.Entities;
using WereldService.WereldStoreDatabaseSettings.authenticationservice.DatastoreSettings;

namespace WereldService.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _users;
        public UserRepository(IWereldstoreDatabaseSettings set)
        {
            var client = new MongoClient(set.ConnectionString);
            var database = client.GetDatabase(set.DatabaseName);

            _users = database.GetCollection<User>(set.UserCollectionName);
        }
        public async Task<User> Create(User user)
        {
            await _users.InsertOneAsync(user);
            return user;
        }

        public async Task<User> Get(Guid id)
        {
            return await _users.Find(user => user.Id == id).FirstOrDefaultAsync();
        }

        public async Task<User> Get(string Name)
        {
            return await _users.Find(user => user.Name == Name).FirstOrDefaultAsync();
        }

        public async Task remove(Guid id)
        {
            await _users.DeleteOneAsync(user => user.Id == id);
        }

        public async Task Update(Guid id, User update)
        {
            await _users.ReplaceOneAsync(user => user.Id == id, update);

        }
    }
}
