using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace WereldService.Helpers
{
    public class AuthenticationHelper : IAuthenticationHelper
    {
        public Guid getUserIdFromToken(string jwt)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(jwt.Replace("Bearer ", String.Empty));
            var idclaim = Guid.Parse((string)token.Payload["unique_name"]);
            return idclaim;

        }
    }
}
