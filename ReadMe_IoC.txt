You may expose the AlertBuilderFactory via IoC to ease the use of the library.  An example
Castle Windsor Installer is below.


using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace DIYMedia.Alerts.Bootstrapping.Windsor
{
    public class ApplicationServicesInstallers : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IAlertBuilderFactory>().ImplementedBy<AlertBuilderFactory>());
        }
    }
}
