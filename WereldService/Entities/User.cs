using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WereldService.Entities
{
    public class User
    {
        //Should be from Authentication Repository
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Guid> WorldFollowed { get; set; }
    }
}
