using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WereldService.Exceptions
{
    public class UserNotOwnerException : Exception
    {
        public UserNotOwnerException() : base("User is not the owner of this world")
        {

        }
    }
}
