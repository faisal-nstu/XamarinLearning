using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using MyXamarinLearnings.Services;
using Prism.Services;
using Xamarin.Forms;

namespace MyXamarinLearnings.ViewModels
{
    public class AccountPageViewModel : BindableBase, INavigationAware
    {
        private INavigationService _navigationService;
        private IDataService _dataService;
        private IPageDialogService _pageDialogService;
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

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        public DelegateCommand<string> TabTappedCommand { get; set; }
        public DelegateCommand LoginCommand { get; set; }

        public AccountPageViewModel(INavigationService navigationService, IDataService dataService, IPageDialogService pageDialogService)
        {
            _navigationService = navigationService;
            _dataService = dataService;
            _pageDialogService = pageDialogService;
            TabNo = 1;
            IsBusy = false;
            TabTappedCommand = new DelegateCommand<string>(TabTapped);
            LoginCommand = new DelegateCommand(Login);
            //LoginCommand = new DelegateCommand(ShowAlert);
        }

        private async void ShowAlert(string message)
        {
            //await _pageDialogService.DisplayAlertAsync("TITLE", "MESSAGE", "OK");
            //var alertButton2 = new Button { Text = "DisplayAlert Yes/No" }; // triggers alert
            await _pageDialogService.DisplayAlertAsync("Success",message,"Ok");
            //Debug.WriteLine("Answer: " + answer); // writes true or false to the console
        }

        private async void Login()
        {
            IsBusy = true;
            var success = false;
            try
            {
                var loginModel = new UserLoginModel()
                {
                    ipAddress = "0.0.0.0",
                    password = "123456Qq",
                    userName = "faisal"
                };
                var response = await _dataService.Login(loginModel);
                if (response.ResponseCode == 100)
                {
                    ShowAlert("Authentication Code: " + response.token);
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                IsBusy = false;
            }
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
