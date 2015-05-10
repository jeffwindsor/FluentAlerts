using System;
using FluentAlerts.Transformers;

namespace FluentAlerts.Specs
{
    public class AlertContext
    {
        public string TestText = "Test Text String";
        public string TestUrl = "Test Url String";
        public object TestValue;
        public readonly object[] TestValues = { "one", 12, DateTime.UtcNow };
        public readonly IFluentAlerts Alerts = new FluentAlerts();
        public IFluentAlertBuilder Builder;
        public IFluentAlert Alert;
        public IFluentAlert OtherAlert;
        public ITransformer Transformer;
        internal TestFluentAlertSettings Settings;

        public Exception CaughtException;
    }
}
 