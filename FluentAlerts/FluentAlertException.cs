using System;


namespace FluentAlerts
{
    public class FluentAlertException: ApplicationException 
    {
        public FluentAlertException(IFluentAlertsBuilder builder) : this(builder.ToAlert()) { }
        public FluentAlertException(IFluentAlertsBuilder builder, Exception inner) : this(builder.ToAlert(), inner) { }
        
        public FluentAlertException(IAlert alert) :base(alert.Title)
        {
            Alert = alert;
        }
        public FluentAlertException(IAlert alert, Exception inner): base(alert.Title, inner)
        {
            Alert = alert;
        }

        public IAlert Alert { get; private set; }
    }
}
