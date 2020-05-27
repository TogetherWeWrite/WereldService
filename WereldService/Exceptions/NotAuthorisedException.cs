using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WereldService.Exceptions
{
    public class NotAuthorisedException : Exception
    { 
        public NotAuthorisedException(string msg) : base(msg) { }
    }
}
