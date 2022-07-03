using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

using Xamarin.Essentials;

using IdentityModel.Client;

using KaraokeApp.Infrastructure.Helper.Configuration;
using KaraokeApp.Core.Services.Identity;
using KaraokeApp.Core.Entities;

namespace KaraokeApp.ViewModels
{
    public class LoginPageViewModel: ViewModelBase
    {
        private bool _isWiFiDisabled;
        public bool IsWiFiDisabled
        {
            get => _isWiFiDisabled;
            private set => SetProperty(ref _isWiFiDisabled, value);
        }

        private string _loginUrl;
        public string LoginUrl
        {
            get => _loginUrl;
            private set => SetProperty(ref _loginUrl, value);
        }

        private bool _isLogin;
        public bool IsLogin
        {
            get => _isLogin;
            private set => SetProperty(ref _isLogin, value);
        }

        private readonly IIdentityService _identityService;
        private readonly IPageDialogService _pageDialog;

        public DelegateCommand LoginCommand { get; private set; }
        public DelegateCommand NewUserCommand { get; private set; }
        public DelegateCommand RecoveryPasswordCommand { get; private set; }
        public DelegateCommand<string> NavigatedCommand { get; private set; }

        public LoginPageViewModel(INavigationService navigationService,
            IPageDialogService pageDialog,
            IIdentityService identityService) : base(navigationService)
        {
            this._identityService = identityService;
            this._pageDialog = pageDialog;

            this.NewUserCommand = new DelegateCommand(NewUser);
            this.LoginCommand = new DelegateCommand(Login);
            this.RecoveryPasswordCommand = new DelegateCommand(RecoveryPassword);
            this.NavigatedCommand = new DelegateCommand<string>(async (url) => await NavigatedAsync(url));

            this.CheckWiFiStatus();
        }

        private void CheckWiFiStatus()
        {
            IEnumerable<ConnectionProfile> profiles = Connectivity.ConnectionProfiles;

            this.IsWiFiDisabled = !profiles.Contains(ConnectionProfile.WiFi);
        }

        private void Login()
        {
            this.LoginUrl = this._identityService.CreateAuthorizationRequest();
            this.IsLogin = true;
        }

        private void NewUser()
        {
            _pageDialog.DisplayAlertAsync("Info", $"{LoginUrl} - {IsLogin}", "Aceptar");
        }

        private void RecoveryPassword()
        {
            _pageDialog.DisplayAlertAsync("Info", "No implementado!", "Aceptar");
        }

        private async Task NavigatedAsync(string url) 
        {
            string unescapedUrl = System.Net.WebUtility.UrlDecode(url);

            if (unescapedUrl.Contains(AppConfigurationManager.Settings["XamarinCallback"]))
            {
                AuthorizeResponse authorizeResponse = new AuthorizeResponse(url);

                if (!string.IsNullOrWhiteSpace(authorizeResponse.Code))
                {
                    UserToken userToken = await _identityService.GetTokenAsync(authorizeResponse.Code);
                    string accessToken = userToken.AccessToken;

                    if (!string.IsNullOrWhiteSpace(accessToken))
                    {
                        await NavigationService.NavigateAsync("NavigationPage/Main/MainPage");
                    }
                }
            }
        }
    }
}
