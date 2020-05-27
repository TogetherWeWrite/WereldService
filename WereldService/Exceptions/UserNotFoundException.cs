using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WereldService.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string msg) : base(msg) { }
    }
}
