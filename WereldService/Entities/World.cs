using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WereldService.Entities
{
    public class World
    {
        [BsonId]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int OwnerId { get; set; }
    }
}
