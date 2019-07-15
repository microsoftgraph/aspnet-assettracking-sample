using System.Net.Http.Headers;
using System.Security.Claims;
using Microsoft.Graph;

namespace AssetTracking.Helpers
{
    public class GraphSdkHelper : IGraphSdkHelper
    {
        private readonly IGraphAuthProvider _authProvider;
        private GraphServiceClient _graphClient;

        public GraphSdkHelper(IGraphAuthProvider authProvider)
        {
            _authProvider = authProvider;
        }
        public GraphServiceClient GetAuthenticatedClient(ClaimsIdentity userIdentity)
        {
            _graphClient = new GraphServiceClient(new DelegateAuthenticationProvider(
                async requestMessage =>
                {
                    
                    var identifier = userIdentity.FindFirst(Startup.ObjectIdentifierType)?.Value + "." + userIdentity.FindFirst(Startup.TenantIdType)?.Value;
                  
                    var accessToken = await _authProvider.GetUserAccessTokenAsync(identifier);

                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                 
                    requestMessage.Headers.Add("SampleID", "aspnet-assettracking-sample");
                }));

            return _graphClient;
        }
    }
    public interface IGraphSdkHelper
    {
       GraphServiceClient GetAuthenticatedClient(ClaimsIdentity userIdentity);
    }
}