using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WereldService.Entities;

namespace WereldService.Helpers
{
    public class UserHelper : IUserHelper
    {
        public Task<User> GetOwnerFromAuthentication(int ownerId)
        {
            throw new NotImplementedException();
        }
    }
}
