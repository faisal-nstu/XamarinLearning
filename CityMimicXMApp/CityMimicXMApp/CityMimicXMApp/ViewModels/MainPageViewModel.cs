using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using CityMimicXMApp.Services;
using Xamarin.Forms;

namespace CityMimicXMApp.ViewModels
{
    public class MainPageViewModel : BindableBase, INavigationAware
    {
        private INavigationService _navigationService;
        private IDataService _dataService;
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private int _tabNo;
        public int TabNo
        {
            get { return _tabNo; }
            set { SetProperty(ref _tabNo, value); }
        }


        public DelegateCommand<string> TabTappedCommand { get; set; }
        public DelegateCommand LoginCommand { get; set; }

        public MainPageViewModel(INavigationService navigationService, IDataService dataService)
        {
            _navigationService = navigationService;
            _dataService = dataService;
            TabNo = 1;
            TabTappedCommand = new DelegateCommand<string>(TabTapped);
            LoginCommand = new DelegateCommand(Login);
        }

        private async void Login()
        {
            var loginModel = new UserLoginModel()
            {
                ipAddress = "0.0.0.0",
                password = "123456Qq",
                userName = "faisal"
            };
            var response = await _dataService.Login(loginModel);
        }

        private void TabTapped(string tabName)
        {
            _navigationService.NavigateAsync(tabName);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("title"))
                Title = (string)parameters["title"] + " and Prism";
        }
    }
}
