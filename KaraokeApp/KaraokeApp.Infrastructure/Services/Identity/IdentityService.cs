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
            return "test";
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
