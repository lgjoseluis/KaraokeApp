using Xamarin.Essentials;
using System.Linq;

using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Collections.Generic;
using KaraokeApp.Core.Services.Identity;

namespace KaraokeApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public bool IsWiFiDisabled { get; private set; }

        private readonly IIdentityService _identityService;
        private readonly IPageDialogService _pageDialog;

        public DelegateCommand LoginCommand { get; private set; }
        public DelegateCommand NewUserCommand { get; private set; }
        public DelegateCommand RecoveryPasswordCommand { get; private set; }

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

            CheckWiFiStatus();
        }

        private void CheckWiFiStatus()
        {
            IEnumerable<ConnectionProfile> profiles = Connectivity.ConnectionProfiles;

            this.IsWiFiDisabled = !profiles.Contains(ConnectionProfile.WiFi);            
        }

        private void Login()
        {
            var value = this._identityService.CreateAuthorizationRequest();

            _pageDialog.DisplayAlertAsync("Info", "Value : " + value, "Aceptar");
        }

        private void NewUser()
        {
            _pageDialog.DisplayAlertAsync("Info", "No implementado!", "Aceptar");
        }

        private void RecoveryPassword()
        {
            _pageDialog.DisplayAlertAsync("Info", "No implementado!", "Aceptar");
        }
    }
}