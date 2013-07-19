using System;

namespace FluentAlerts
{
    public class AlertException: ApplicationException 
    {
        public AlertException(IAlertBuilder builder) : this(builder.ToAlert()) { }
        public AlertException(IAlertBuilder builder, Exception inner) : this(builder.ToAlert(), inner) { }
        
        public AlertException(IAlert alert) :base(alert.Title)
        {
            Alert = alert;
        }
        public AlertException(IAlert alert, Exception inner): base(alert.Title, inner)
        {
            Alert = alert;
        }

        public IAlert Alert { get; private set; }
    }
}
