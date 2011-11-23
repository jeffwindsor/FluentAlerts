using System;

namespace Alerts
{
    public class AlertException: ApplicationException 
    {
        public AlertException(string message) : this(new AlertTextBlock(message)) { }
        public AlertException(string message, Exception inner) : this(new AlertTextBlock(message), inner) { }
        public AlertException(IAlertBuilder builder) : this(builder.ToAlert()){}
        public AlertException(IAlertBuilder builder, Exception inner) : this(builder.ToAlert(), inner){}
        public AlertException(IAlert alert):base(alert.ToText()){ Alert = alert; }
        public AlertException(IAlert alert, Exception inner) : base(alert.ToText(),inner){ Alert = alert; }
        public IAlert Alert { get; set; }
    }
}
