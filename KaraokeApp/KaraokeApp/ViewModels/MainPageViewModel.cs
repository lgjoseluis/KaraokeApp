using Prism.Navigation;

namespace KaraokeApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Página principal";
        }
    }
}