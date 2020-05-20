using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WereldService.Entities;
using System.Net.Http.Headers;
using MongoDB.Bson;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WereldService.Exceptions;
using WereldService.Models;

namespace WereldService.Helpers
{
    public class UserHelper : IUserHelper
    {
        public async Task<User> GetOwnerFromAuthentication(Guid ownerId)
        {
            string URL = "https://localhost:5001/Account";
            string urlParameters = "?id=" + ownerId;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                HttpResponseMessage response = await client.GetAsync(urlParameters);
                if (response.IsSuccessStatusCode)
                {
                    //Succesfully Arrives.
                    var obj = await response.Content.ReadAsStringAsync();
                    JObject jsonObject = JObject.Parse(obj);
                    var user = new User
                    {
                        Id = (Guid)jsonObject["id"],
                        Name = (string)jsonObject["name"]
                    };
                    return user;
                }
                else
                {
                    throw new UserDoesNotExistInAuthenticationServiceException("The user with the Id: " + ownerId + " Does not exist");
                }
            }
            catch (Exception)
            {
                throw new AuthenticationServiceIsNotOnlineException("Authentication service is not online atm");
            }
        }
    }
}
