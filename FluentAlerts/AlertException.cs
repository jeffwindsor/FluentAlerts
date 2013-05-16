using System;

namespace FluentAlerts
{
    public class AlertException: ApplicationException 
    {
        public AlertException(string message)
            : base(message)
        {
            Alert = GetAlertWithTitle(message);
        }
        public AlertException(string message, Exception inner) 
            : base(message, inner)
        {
            Alert = GetAlertWithTitle(message);
        }
        
        public AlertException(IAlertBuilder builder) 
            : this(builder.ToAlert()) { }
        public AlertException(IAlertBuilder builder, Exception inner) 
            : this(builder.ToAlert(), inner) { }

        public AlertException(IAlert alert) :base(alert.Title)
        {
            Alert = alert;
        }
        public AlertException(IAlert alert, Exception inner): base(alert.Title, inner)
        {
            Alert = alert;
        }

        public IAlert Alert { get; set; }

        //TODO: Map to pre-serialization, and serilization? for ease of transformations


        private static IAlert GetAlertWithTitle(string message)
        {
            return Alerts.Create(message).ToAlert();
        }
    }
}
