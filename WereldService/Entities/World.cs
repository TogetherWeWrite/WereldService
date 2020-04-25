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
        public int OwnerId { get; set; }
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
    }
}

