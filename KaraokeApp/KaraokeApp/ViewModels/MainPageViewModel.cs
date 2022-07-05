
using System.Threading.Tasks;

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
    public class MainPageViewModel : ViewModelBase
    {
        private string _idToken;

        private readonly IPageDialogService _pageDialog;

        public DelegateCommand LogoutCommand { get; private set; }

        public MainPageViewModel(INavigationService navigationService,
            IPageDialogService pageDialog) : base(navigationService)
        {
            this.Title = "Página principal";

            this._pageDialog = pageDialog;

            this.LogoutCommand = new DelegateCommand(async () => await Logout());
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            this._idToken = parameters.GetValue<string>("IdToken");

            //_pageDialog.DisplayAlertAsync("Info", this._idToken, "Aceptar");
        }

        private async Task Logout()
        {
            INavigationParameters parameters = new NavigationParameters
            {
                { "Logout", true },
                { "IdToken", this._idToken }
            };

            await this.NavigationService.NavigateAsync("LoginPage", parameters);
        }
    }
}