using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WereldService.Exceptions
{
    public class UserIsAlreadyAWriterException : Exception
    {
        public UserIsAlreadyAWriterException(string msg) : base(msg)
        {

        }
    }
}
