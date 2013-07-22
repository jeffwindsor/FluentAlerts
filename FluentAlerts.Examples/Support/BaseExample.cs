using NUnit.Framework;

namespace FluentAlerts.Examples
{
    public abstract class BaseExample
    {
        //Simulate IOC or other creation methods in your code
        protected readonly IAlertBuilderFactory _alerts = ObjectFactory.CreateDefaultAlertBuilderFactory();

    }
}
