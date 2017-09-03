using Autofac;
using Drone.Services.Interface;

namespace Drone.Services.Autofac
{
    public class RegisterTypes
    {
        public static void Register(ContainerBuilder builder)
        {
            builder.RegisterType<SettingsService>().As<ISettingsService>().InstancePerLifetimeScope();
            builder.RegisterType<DroneNavigationService>().As<IDroneNavigationService>().InstancePerLifetimeScope();
        }
    }
}
