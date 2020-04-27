using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WereldService.Exceptions
{
    public class WriterDoesNotExistInWorldException : Exception
    {
        public WriterDoesNotExistInWorldException(string msg) : base(msg)
        {

        }
    }
}
