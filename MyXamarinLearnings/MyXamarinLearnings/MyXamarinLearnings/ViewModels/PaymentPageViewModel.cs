using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;

namespace MyXamarinLearnings.ViewModels
{
    public class PaymentPageViewModel : BindableBase
    {
        private INavigationService _navigationService;

        private int _tabNo;
        public int TabNo
        {
            get { return _tabNo; }
            set { SetProperty(ref _tabNo, value); }
        }

        public DelegateCommand<string> TabTappedCommand { get; set; }
        public PaymentPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            TabNo = 3;
            TabTappedCommand = new DelegateCommand<string>(TabTapped);
        }

        private void TabTapped(string tabName)
        {
            _navigationService.NavigateAsync(tabName);
        }
    }
}
