using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WereldService.Entities;

namespace WereldService.Models
{
    public class WorldWithDetails
    {
        public Guid WorldId { get; set; }
        public string Title { get; set; }
        public Owner Owner { get; set; }
        public List<Writer> Writers { get; set; }
    }

    public class Owner
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class Writer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class WorldWithDetailsAndFollowers : WorldWithDetails
    {
        public List<User> Followers { get; set; }
    }
}
