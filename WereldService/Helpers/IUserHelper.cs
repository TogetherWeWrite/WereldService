using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WereldService.Entities;

namespace WereldService.Helpers
{
    public interface IUserHelper
    {
        //TODO Update this whole process to work with a message queue        
        /// <summary>
        /// Gets the owner from the authenticationRepository with name
        /// </summary>
        /// <param name="ownerId">Id of the owner</param>
        /// <returns>User  with name and Id from the authentication service</returns>
        /// <exception cref="UserDoesNotExistInAuthenticationServiceException">This exception will be throw if the user does not exist in the authentication service.</exception>
        /// <exception cref="AuthenticationServiceIsNotOnlineException">This exception will be throw if the user does not exist in the authentication service.</exception>
        Task<User> GetOwnerFromAuthentication(Guid ownerId);
    }
}
