using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WereldService.Entities;

namespace WereldService.Helpers
{
    public class UserHelper : IUserHelper
    {
        public async Task<User> GetOwnerFromAuthentication(int ownerId)
        {
            return new User
            {
                Id = ownerId,
                Name = "GetOwnerFromAuthentication boi"
            };
        }
    }
}
