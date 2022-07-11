
using System.Threading.Tasks;

using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

using Xamarin.Essentials;

using IdentityModel.Client;

using KaraokeApp.Infrastructure.Helper.Configuration;
using KaraokeApp.Core.Services.Identity;
using KaraokeApp.Core.Entities;
using KaraokeApp.Core.Services.Settings;

namespace KaraokeApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly ISettingsService _settingsService;
        private readonly IPageDialogService _pageDialog;

        public DelegateCommand LogoutCommand { get; private set; }

        public MainPageViewModel(INavigationService navigationService,
            IPageDialogService pageDialog,
            ISettingsService settingsService) : base(navigationService)
        {
            this.Title = "Página principal";

            this._settingsService = settingsService;
            this._pageDialog = pageDialog;

            this.LogoutCommand = new DelegateCommand(async () => await Logout());
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            string idToken = this._settingsService.AuthIdToken;

            _pageDialog.DisplayAlertAsync("Info", idToken, "Aceptar");
        }

        private async Task Logout()
        {
            INavigationParameters parameters = new NavigationParameters
            {
                { "Logout", true }
            };

            await this.NavigationService.NavigateAsync("app:///LoginPage", parameters);
        }
    }
}