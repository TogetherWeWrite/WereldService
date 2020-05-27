using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WereldService.Models
{
    public class WorldFollowModel
    {
        public Guid UserId { get; set; }
        public Guid WorldId { get; set; }
        public bool Follow { get; set; }
    }
}
