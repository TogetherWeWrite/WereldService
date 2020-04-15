using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WereldService.Exceptions
{
    public class WorldNotFoundException : Exception
    {
        public WorldNotFoundException(string msg) : base(msg)
        {
        }
    }
}
