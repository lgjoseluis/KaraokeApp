using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using System.Net;

using KaraokeApp.Core.Services.Identity;
using KaraokeApp.Core.Entities;
using KaraokeApp.Infrastructure.Helper.Configuration;
using KaraokeApp.Core.Services.RequestProvider;

namespace KaraokeApp.Infrastructure.Services.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly IRequestProvider _requestProvider;

        public IdentityService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

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
            if (string.IsNullOrEmpty(token))
            {
                return string.Empty;
            }

            string logoutEndpoint = $"{AppConfigurationManager.Settings["AuthorizeEndpoint"]}/logout";
            string logoutCallback = AppConfigurationManager.Settings["LogoutCallback"];

            return string.Format("{0}?id_token_hint={1}&post_logout_redirect_uri={2}",
                logoutEndpoint,
                token,
                logoutCallback);
        }

        public async Task<UserToken> GetTokenAsync(string code)
        {
            string data = string.Format("grant_type=authorization_code&code={0}&redirect_uri={1}", code, WebUtility.UrlEncode(AppConfigurationManager.Settings["XamarinCallback"]));
            string tokenEndpoint = $"{AppConfigurationManager.Settings["AuthorizeEndpoint"]}/token";
            string clientId = AppConfigurationManager.Settings["ClientId"];
            string clientSecret = AppConfigurationManager.Settings["ClientSecret"];

            var token = await _requestProvider.PostAsync<UserToken>(tokenEndpoint, data, clientId, clientSecret);

            return token;
        }
    }
}
