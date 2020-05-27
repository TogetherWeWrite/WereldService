using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WereldService.Helpers
{
    public interface IAuthenticationHelper
    {
        public Guid getUserIdFromToken(string jwt);
    }
}
