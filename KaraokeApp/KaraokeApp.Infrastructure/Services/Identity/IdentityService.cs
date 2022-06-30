using KaraokeApp.Core.Services.Identity;
using KaraokeApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KaraokeApp.Infrastructure.Helper.Configuration;

namespace KaraokeApp.Infrastructure.Services.Identity
{
    public class IdentityService : IIdentityService
    {
        public string CreateAuthorizationRequest()
        {
            string url = $"{AppConfigurationManager.Settings["AuthorizeEndpoint"]}/auth";

            // Create URI to authorization endpoint
            AuthorizeRequest authorizeRequest = new AuthorizeRequest(url);

            // Dictionary with values for the authorize request
            Dictionary<string, string> dic = new Dictionary<string, string>
            {
                { "client_id", AppConfigurationManager.Settings["ClientId"] },
                { "response_type", "code id_token" },
                { "nonce", Guid.NewGuid().ToString("N") }
            };

            // Add CSRF token to protect against cross-site request forgery attacks.
            string  currentCSRFToken = Guid.NewGuid().ToString("N");

            dic.Add("state", currentCSRFToken);

            string authorizeUri = authorizeRequest.Create(dic);

            return authorizeUri;
        }

        public string CreateLogoutRequest(string token)
        {
            throw new NotImplementedException();
        }

        public Task<UserToken> GetTokenAsync(string code)
        {
            throw new NotImplementedException();
        }
    }
}
