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

            NavigationService.NavigateAsync("Page1");
        }

        

        protected override void RegisterTypes()
        {
            Container.RegisterType<IDataService, DataService>(new ContainerControlledLifetimeManager());
            Container.RegisterTypeForNavigation<Page1>();
            Container.RegisterTypeForNavigation<Page2>();
            Container.RegisterTypeForNavigation<Page3>();
            Container.RegisterTypeForNavigation<Page4>();
        }
    }
}
