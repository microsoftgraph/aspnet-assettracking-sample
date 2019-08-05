using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Graph;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AssetTracking.Helpers
{
    public static class GraphService
    {       
        public static async Task<string> GetUserJson(GraphServiceClient graphClient, string email, HttpContext httpContext)
        {
            if (email == null) return JsonConvert.SerializeObject(new { Message = "Email address cannot be null." }, Formatting.Indented);

            try
            {

                var user = await graphClient.Users[email].Request().GetAsync();
                {
                    return JsonConvert.SerializeObject(user, Formatting.Indented);
                }
            }
            catch (ServiceException e)
            {
                switch (e.Error.Code)
                {
                    case "Request_ResourceNotFound":
                    case "ResourceNotFound":
                    case "ErrorItemNotFound":
                    case "itemNotFound":
                        return JsonConvert.SerializeObject(new { Message = $"User '{email}' was not found." }, Formatting.Indented);

                    case "ErrorInvalidUser":
                        return JsonConvert.SerializeObject(new { Message = $"The requested user '{email}' is invalid." }, Formatting.Indented);

                    case "AuthenticationFailure":
                        return JsonConvert.SerializeObject(new { e.Error.Message }, Formatting.Indented);

                    case "TokenNotFound":
                        await httpContext.ChallengeAsync();
                        return JsonConvert.SerializeObject(new { e.Error.Message }, Formatting.Indented);

                    default:
                        return JsonConvert.SerializeObject(new { Message = "An unknown error has occurred." }, Formatting.Indented);
                }
            }
        }
    }
}