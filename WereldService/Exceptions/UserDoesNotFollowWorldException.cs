using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WereldService.Exceptions
{
    public class UserDoesNotFollowWorldException : Exception
    {
        public UserDoesNotFollowWorldException(string msg) : base(msg) { }
    }
}
