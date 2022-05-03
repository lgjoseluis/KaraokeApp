using Prism.Mvvm;
using Prism.Navigation;

namespace KaraokeApp.ViewModels
{
    public abstract class ViewModelBase : BindableBase, INavigationAware, IDestructible
    {
        protected INavigationService NavigationService { get; }

        protected ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private bool _isBusy = false;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        private bool _canNavigate = true;
        public bool CanNavigate
        {
            get => _canNavigate;
            set => SetProperty(ref _canNavigate, value);
        }

        private bool _isEnable = true;
        public bool IsEnable
        {
            get => _isEnable;
            set => SetProperty(ref _isEnable, value);
        }

        // INavigationAware
        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        // INavigationAware
        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        // IDestructible
        public virtual void Destroy()
        {

        }
    }
}

