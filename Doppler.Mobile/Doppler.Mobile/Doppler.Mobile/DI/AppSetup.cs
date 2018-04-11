using Autofac;
using Doppler.Mobile.Core.Configuration;
using Doppler.Mobile.Core.Networking;
using Doppler.Mobile.Core.Services;
using Doppler.Mobile.Core.Settings;
using Doppler.Mobile.Navigation;
using Doppler.Mobile.ViewModels;
using Plugin.Settings;
using Xamarin.Forms;

namespace Doppler.Mobile.DI
{
    public class AppSetup
    {
        public IContainer CreateContainer()
        {
            var containerBuilder = new ContainerBuilder();
            RegisterDependencies(containerBuilder);
            return containerBuilder.Build();
        }

        protected virtual void RegisterDependencies(ContainerBuilder cb)
        {
            // Settings
            cb.Register(c => new LocalSettings(CrossSettings.Current)).As<ILocalSettings>();
            cb.RegisterType<ConfigurationSettings>().As<IConfigurationSettings>().SingleInstance();

            //Networking
            cb.RegisterType<DopplerAPI>().As<IDopplerAPI>().SingleInstance();

            // Services
            cb.RegisterType<AuthenticationService>().As<IAuthenticationService>();

            cb.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();

            // View Models
            cb.RegisterType<CampaignFeedViewModel>();
            cb.RegisterType<AuthenticationViewModel>();

        }
    }
}
