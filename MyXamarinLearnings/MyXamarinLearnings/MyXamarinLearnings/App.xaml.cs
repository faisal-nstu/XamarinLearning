using MyXamarinLearnings.Services;
using Prism.Unity;
using MyXamarinLearnings.Views;
using Microsoft.Practices.Unity;

namespace MyXamarinLearnings
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();

            NavigationService.NavigateAsync("AccountPage?title=Hello%20from%20Xamarin.Forms");
        }
        

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<AccountPage>();
            Container.RegisterTypeForNavigation<PaymentPage>();
            Container.RegisterType<IDataService, DataService>(new ContainerControlledLifetimeManager());
            Container.RegisterTypeForNavigation<PrismContentPage1>();
        }
    }
}
