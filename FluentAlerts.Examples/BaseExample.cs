using System.Diagnostics;
using Ninject;
using Ninject.Modules;

namespace FluentAlerts.Examples
{
    public abstract class BaseExample
    {
        private readonly IKernel _container;
        protected BaseExample()
        {
            _container = new StandardKernel(new ExamplesNinjectModule());
        }

        protected T Get<T>()
        {
            return _container.Get<T>();
        }
        protected static void SendToConsole(string value)
        {
            Trace.WriteLine(value);
        }
    }


    public class ExamplesNinjectModule : NinjectModule
    {
        public override void Load()
        {
            //Default IoC Bindings
            Bind<IFluentAlerts>().To<FluentAlertFactory>();
            Bind<IFluentAlertSerializer>().To<FluentAlertSerializer>();
        }
    }
}
