using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using MyXamarinLearnings.Services;
using Prism.Navigation;

namespace MyXamarinLearnings.ViewModels
{
    public class Page3ViewModel : BindableBase
    {
        private INavigationService _navigationService;
        private IDataService _dataService;
        public DelegateCommand<string> ChangeTab { get; set; }
        public Page3ViewModel(INavigationService navigationService, IDataService dataService)
        {
            _navigationService = navigationService;
            _dataService = dataService;

            ChangeTab = new DelegateCommand<string>(delegate (string page) { _navigationService.NavigateAsync(page); });
        }
    }
}
