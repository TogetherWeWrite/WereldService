using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WereldService.Exceptions
{
    public class WorldAlreadyFollowedException : Exception
    {
        public WorldAlreadyFollowedException(string msg) : base(msg) { }
    }
}
