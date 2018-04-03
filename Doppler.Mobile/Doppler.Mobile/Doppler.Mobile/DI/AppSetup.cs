using Autofac;
using Doppler.Mobile.Core.DI;
using Doppler.Mobile.Core.Services;
using Doppler.Mobile.ViewModels;

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
            // Core modules
            cb.RegisterModule<CoreSetup>();

            // View Models
            cb.RegisterType<HomeViewModel>();
            cb.RegisterType<AuthenticationViewModel>();

        }
    }
}
