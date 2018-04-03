using System;
using Autofac;
using Plugin.Settings;
using Doppler.Mobile.Core.Settings;
using Doppler.Mobile.Core.Services;
using Doppler.Mobile.Core.Networking;
using Doppler.Mobile.Core.Configuration;

namespace Doppler.Mobile.Core.DI
{
    public class CoreSetup : Module
    {
        protected override void Load(ContainerBuilder cb)
        {
            // Settings
            cb.Register(c => new LocalSettings(CrossSettings.Current)).As<ILocalSettings>();
            cb.RegisterType<ConfigurationSettings>().As<IConfigurationSettings>().SingleInstance();
            cb.RegisterType<DopplerAPI>().As<IDopplerAPI>().SingleInstance();

            // Services
            cb.RegisterType<AuthenticationService>().As<IAuthenticationService>();
        }
    }
}
