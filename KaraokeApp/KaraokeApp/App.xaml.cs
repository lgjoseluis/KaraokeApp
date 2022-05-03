using KaraokeApp.ViewModels;
using KaraokeApp.Views;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using Xamarin.Forms;

namespace KaraokeApp
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer platformInitializer = null) : base(platformInitializer)
        {

        }

        protected override void OnStart()
        {
            // Method intentionally left empty.
        }

        protected override void OnSleep()
        {
            // Method intentionally left empty.
        }

        protected override void OnResume()
        {
            // Method intentionally left empty.
        }

        protected override void OnInitialized()
        {
            InitializeComponent();

            NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
        }
    }
}
