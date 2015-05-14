using System.Diagnostics;

namespace FluentAlerts.Examples
{
    public abstract class BaseExample
    {
        //private readonly IKernel _container;
        private readonly IFluentAlerts _alerts;
        protected BaseExample()
        {
            //_container = new StandardKernel(new ExamplesIocConfig());
            //_alerts = _container.Get<IFluentAlerts>();
            _alerts= new FluentAlerts();
        }
        protected IFluentAlerts Alerts
        {
            get { return _alerts; }
        }
        //protected T Get<T>()
        //{
        //    return _container.Get<T>();
        //}
        protected static void SerializeToConsole(object o, IFluentAlertSerializer serializer)
        {
            SendToConsole(serializer.Serialize(o));
        }
        protected static void SendToConsole(string value)
        {
            Trace.WriteLine(value);
        }
    }
}
