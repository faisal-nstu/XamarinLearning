using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using MyXamarinLearnings.Models;
using MyXamarinLearnings.Services;
using Prism.Navigation;
using Xamarin.Forms;

namespace MyXamarinLearnings.ViewModels
{
    public class Page2ViewModel : BindableBase
    {
        private INavigationService _navigationService;
        private IDataService _dataService;
        private int _timerCount = 0;

        private string _address;
        public string Address
        {
            get { return _address; }
            set
            {
                SetProperty(ref _address, value);
                CheckTimer();
            }
        }
        public DelegateCommand<string> ChangeTab { get; set; }
        public Page2ViewModel(INavigationService navigationService, IDataService dataService)
        {
            _navigationService = navigationService;
            _dataService = dataService;

            ChangeTab = new DelegateCommand<string>(delegate (string page) { _navigationService.NavigateAsync(page); });
        }

        

        private void CheckTimer()
        {
            _timerCount = 0;
            Device.StartTimer(new TimeSpan(0, 0, 0), HandleTimer);
        }

        private bool HandleTimer()
        {
            if (_timerCount == 2)
            {
                GetLocations();
                return false;
            }
            _timerCount++;
            return true;
        }

        private ObservableCollection<Prediction> _predictions;
        public ObservableCollection<Prediction> Predictions
        {
            get { return _predictions; }
            set { SetProperty(ref _predictions, value); }
        }

        private async void GetLocations()
        {
            if (string.IsNullOrEmpty(Address) || string.IsNullOrWhiteSpace(Address))
            {
                Predictions = new ObservableCollection<Prediction>();
                return;
            }
            try
            {
                var response = await _dataService.PredictAddress(Address);
                Debug.WriteLine("======================================");
                foreach (var prediction in response.predictions)
                {
                    Debug.WriteLine(prediction.description);
                }
                Debug.WriteLine("======================================");

                if (response.predictions != null && response.predictions.Count != 0)
                {
                    Predictions = new ObservableCollection<Prediction>(response.predictions);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
