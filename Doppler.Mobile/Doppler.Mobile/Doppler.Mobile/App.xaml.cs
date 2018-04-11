using Autofac;
using Doppler.Mobile.DI;
using Doppler.Mobile.Navigation;
using Xamarin.Forms;

namespace Doppler.Mobile
{
    public partial class App : Application
    {
        public App ()
        {
            //InitializeComponent();
            var appSetup = new AppSetup();
            AppContainer.Container = appSetup.CreateContainer();
            var navigationService = AppContainer.Container.Resolve<INavigationService>();
            navigationService.InitializeAsync();
        }

        protected override void OnStart ()
        {
            // Handle when your app starts
        }

        protected override void OnSleep ()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume ()
        {
            // Handle when your app resumes
        }
    }
}
