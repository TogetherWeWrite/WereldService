using Microsoft.CodeAnalysis.CSharp.Syntax;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WereldService.Models;

namespace WereldService.Entities
{
    public class World
    {
        [BsonId]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public User Owner { get; set; }
        public List<User> Writers { get; set; }
        public List<User> Followers { get; set; }

        public void AddWriter(User writer)
        {
            Writers.Add(writer);
        }

        public void RemoveWriter(int index)
        {
            
        }
    }

    public static class ExtensionMethods
    {
        public static WorldOverviewModel ToWorldOverviewModel(this World world)
        {
            return new WorldOverviewModel()
            {
                Title = world.Title,
                WorldId = world.Id
            };

        }
        public static List<WorldOverviewModel> ToWorldOverviewModelList(this List<World> world)
        {
            var worldList = new List<WorldOverviewModel>();
            world.ForEach(world => worldList.Add(world.ToWorldOverviewModel()));
            return worldList;
        }

        public static WorldWithDetails ToDetailWorldModel(this World world)
        {
            List<Writer> writers = new List<Writer>();
            world.Writers.ForEach(user => writers.Add(new Writer
            {
                Name = user.Name,
                Id = user.Id
            }));
            return new WorldWithDetails
            {
                Owner = new Owner
                {
                    Id = world.Owner.Id,
                    Name = world.Owner.Name
                },
                Title = world.Title,
                WorldId = world.Id,
                Writers = writers
            };
        }

        public static WorldWithDetailsAndFollowers ToDetailAndFollowersWorldModel(this World world)
        {
            List<Writer> writers = new List<Writer>();
            world.Writers.ForEach(user => writers.Add(new Writer
            {
                Name = user.Name,
                Id = user.Id
            }));
            return new WorldWithDetailsAndFollowers
            {
                Owner = new Owner
                {
                    Id = world.Owner.Id,
                    Name = world.Owner.Name
                },
                Title = world.Title,
                WorldId = world.Id,
                Writers = writers,
                Followers = world.Followers
            };
        }

        public static List<WorldWithDetails> ToWorldWithDetailsList(this List<World> worlds)
        {
            var worldWithDetailsList = new List<WorldWithDetails>();
            worlds.ForEach(world => worldWithDetailsList.Add(world.ToDetailWorldModel()));
            return worldWithDetailsList;
        }

        public static List<WorldWithDetailsAndFollowers> ToWorldWithDetailsAndFollowersList(this List<World> worlds)
        {
            var worldWithDetailsAndFollowersList = new List<WorldWithDetailsAndFollowers>();
            worlds.ForEach(world => worldWithDetailsAndFollowersList.Add(world.ToDetailAndFollowersWorldModel()));
            return worldWithDetailsAndFollowersList;
        }
    }
}

