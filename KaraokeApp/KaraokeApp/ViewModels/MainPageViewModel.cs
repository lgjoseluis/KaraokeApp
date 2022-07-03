
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
    public class MainPageViewModel : ViewModelBase
    {   
        private readonly IPageDialogService _pageDialog;               

        public MainPageViewModel(INavigationService navigationService,
            IPageDialogService pageDialog) : base(navigationService)
        {
            this.Title = "Página principal";            
        }

        
    }
}