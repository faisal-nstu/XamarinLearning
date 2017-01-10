using CityMimicXMApp.Services;
using Prism.Unity;
using CityMimicXMApp.Views;
using Microsoft.Practices.Unity;

namespace CityMimicXMApp
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();

            NavigationService.NavigateAsync("MainPage?title=Hello%20from%20Xamarin.Forms");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<MainPage>();
            Container.RegisterTypeForNavigation<PaymentPage>();
            Container.RegisterType<IDataService, DataService>(new ContainerControlledLifetimeManager());
        }
    }
}
