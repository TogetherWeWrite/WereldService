using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WereldService.Exceptions
{
    public class AuthenticationServiceIsNotOnlineException : Exception
    {
        public AuthenticationServiceIsNotOnlineException(string msg) : base(msg)
        {

        }
    }
}
