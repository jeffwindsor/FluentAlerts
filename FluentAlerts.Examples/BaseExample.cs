using System.Diagnostics;
using Ninject;

namespace FluentAlerts.Examples
{
    public abstract class BaseExample
    {
        private readonly IKernel _container;
        protected BaseExample()
        {
            _container = new StandardKernel(new ExamplesIocConfig());
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
}
