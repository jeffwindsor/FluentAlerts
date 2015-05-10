using System;


namespace FluentAlerts
{
    public class FluentAlertException: ApplicationException 
    {
        public FluentAlertException(IFluentAlertBuilder builder) : this(builder.ToAlert()) { }
        public FluentAlertException(IFluentAlertBuilder builder, Exception inner) : this(builder.ToAlert(), inner) { }
        
        public FluentAlertException(IFluentAlert alert) :base(alert.Title)
        {
            Alert = alert;
        }
        public FluentAlertException(IFluentAlert alert, Exception inner): base(alert.Title, inner)
        {
            Alert = alert;
        }

        public IFluentAlert Alert { get; private set; }
    }
}
