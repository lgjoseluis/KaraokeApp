
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

using Xamarin.Essentials;

using KaraokeApp.Infrastructure.Helper.Configuration;
using KaraokeApp.Core.Services.Identity;

namespace KaraokeApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
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
        public DelegateCommand<string> NavigateCommand { get; private set; }

        public MainPageViewModel(INavigationService navigationService,
            IPageDialogService pageDialog,
            IIdentityService identityService) : base(navigationService)
        {
            Title = "Página principal";

            this._identityService = identityService;
            this._pageDialog = pageDialog;

            this.NewUserCommand = new DelegateCommand(NewUser);
            this.LoginCommand = new DelegateCommand(Login);
            this.RecoveryPasswordCommand = new DelegateCommand(RecoveryPassword);
            this.NavigateCommand = new DelegateCommand<string>(async (url) => await NavigateAsync(url));

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

        private async Task NavigateAsync(string url)
        {
            string unescapedUrl = System.Net.WebUtility.UrlDecode(url);

            if (unescapedUrl.Contains(AppConfigurationManager.Settings["XamarinCallback"]))
            {
                await _pageDialog.DisplayAlertAsync("Info", "Redirect user", "Aceptar");
            }
        }
    
    }
}