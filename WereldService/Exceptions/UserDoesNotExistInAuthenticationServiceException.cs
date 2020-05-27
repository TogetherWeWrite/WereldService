using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WereldService.Exceptions
{
    public class UserDoesNotExistInAuthenticationServiceException : Exception
    {
        public UserDoesNotExistInAuthenticationServiceException(string msg) : base(msg)
        {

        }
    }
}
