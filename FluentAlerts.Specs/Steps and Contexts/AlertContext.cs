using System;

namespace FluentAlerts.Specs
{
    public class AlertContext
    {
        public string TestText = "Test Text String";
        public string TestUrl = "Test Url String";
        public object TestValue;
        public readonly object[] TestValues = new object[] { "one", 12, DateTime.UtcNow };

        public IAlertBuilder Builder;
        public IAlert Alert;
        public IAlert OtherAlert;
        
    }
}
 