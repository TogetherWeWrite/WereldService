using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WereldService.Exceptions
{
    public class VariablesDoNotMatchException : Exception
    {
        public VariablesDoNotMatchException(string msg) : base(msg)
        {
        }
    }
}
