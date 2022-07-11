using KaraokeApp.Core.Services.Settings;
using System;
using Xamarin.Essentials;

namespace KaraokeApp.Infrastructure.Services.Settings
{
    public class SettingsService : ISettingsService
    {
        private const string AccessToken = "access_token";
        private const string IdToken = "id_token";

        private readonly string AccessTokenDefault = string.Empty;
        private readonly string IdTokenDefault = string.Empty;

        public string AuthAccessToken 
        { 
            get => Preferences.Get(AccessToken, AccessTokenDefault);
            set => Preferences.Set(AccessToken, value);
        }

        public string AuthIdToken 
        {
            get => Preferences.Get(IdToken, IdTokenDefault);
            set => Preferences.Set(IdToken, value);
        }
    }
}
