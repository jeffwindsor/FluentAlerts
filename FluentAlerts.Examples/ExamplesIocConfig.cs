using Ninject.Modules;

namespace FluentAlerts.Examples
{
    public class ExamplesIocConfig : NinjectModule
    {
        public override void Load()
        {
            //Default IoC Bindings
            Bind<IFluentAlerts>().To<FluentAlerts>();
            Bind<IFluentAlertHtmlSerializer>().To<FluentAlertHtmlSerializer>();
        }
    }
}