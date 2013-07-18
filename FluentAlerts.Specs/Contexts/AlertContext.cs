using System;
using FluentAlerts.Transformers;

namespace FluentAlerts.Specs
{
    public class AlertContext
    {
        public string TestText = "Test Text String";
        public string TestUrl = "Test Url String";
        public object TestValue;
        public readonly object[] TestValues = new object[] { "one", 12, DateTime.UtcNow };

        public readonly IAlertBuilderFactory AlertBuilderFactory = new AlertBuilderFactory(new AlertFactory<Alert>());

        public IAlertBuilder Builder;
        public IAlert Alert;
        public IAlert OtherAlert;
        public ITransformer<string> Transformer;
        internal TestFluentAlertSettings Settings;

        public Exception CaughtException;
    }
}
 