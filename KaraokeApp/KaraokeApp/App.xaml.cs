using Xamarin.Forms;

using Prism;
using Prism.Ioc;
using Prism.Unity;

using KaraokeApp.Core.Services.Identity;
using KaraokeApp.Core.Services.RequestProvider;
using KaraokeApp.Infrastructure.Services.Identity;
using KaraokeApp.Infrastructure.Services.RequestProvider;
using KaraokeApp.ViewModels;
using KaraokeApp.Views;

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

            NavigationService.NavigateAsync("LoginPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();

            containerRegistry.RegisterSingleton<IIdentityService, IdentityService>();
            containerRegistry.RegisterSingleton<IRequestProvider, RequestProvider>();
        }
    }
}
