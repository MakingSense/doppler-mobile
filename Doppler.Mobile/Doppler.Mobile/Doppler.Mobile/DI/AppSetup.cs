using Autofac;
using Doppler.Mobile.Core.Configuration;
using Doppler.Mobile.Core.Networking;
using Doppler.Mobile.Core.Services;
using Doppler.Mobile.Core.Settings;
using Doppler.Mobile.ViewModels;
using Plugin.Settings;

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
            cb.RegisterType<DopplerAPI>().As<IDopplerAPI>().SingleInstance();

            // Services
            cb.RegisterType<AuthenticationService>().As<IAuthenticationService>();

            // View Models
            cb.RegisterType<HomeViewModel>();
            cb.RegisterType<AuthenticationViewModel>();

        }
    }
}
