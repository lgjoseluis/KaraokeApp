using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace KaraokeApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly IPageDialogService _pageDialog;

        public DelegateCommand LoginCommand { get; private set; }
        public DelegateCommand NewUserCommand { get; private set; }
        public DelegateCommand RecoveryPasswordCommand { get; private set; }

        public MainPageViewModel(INavigationService navigationService
            , IPageDialogService pageDialog) : base(navigationService)
        {
            Title = "Página principal";

            this._pageDialog = pageDialog;

            this.NewUserCommand = new DelegateCommand(NewUser);
            this.LoginCommand = new DelegateCommand(Login);
            this.RecoveryPasswordCommand = new DelegateCommand(RecoveryPassword);
        }

        private void Login()
        {
            _pageDialog.DisplayAlertAsync("Info", "Evento click!", "Aceptar");
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