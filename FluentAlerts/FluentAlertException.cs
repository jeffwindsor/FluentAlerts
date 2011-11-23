using System;
using FluentAlerts.Extensions;
using FluentAlerts.Nodes;

namespace FluentAlerts
{
    public class FluentAlertException: ApplicationException 
    {
        public FluentAlertException(string message) : this(new FluentAlertTextBlock(message)) { }
        public FluentAlertException(string message, Exception inner) : this(new FluentAlertTextBlock(message), inner) { }
        public FluentAlertException(IFluentAlertBuilder builder) : this(builder.ToAlert()){}
        public FluentAlertException(IFluentAlertBuilder builder, Exception inner) : this(builder.ToAlert(), inner) { }
        public FluentAlertException(IFluentAlert alert)
            :base(alert.ToText(s=>s.Replace(Environment.NewLine, string.Empty)))
        {
            Alert = alert;
        }
        public FluentAlertException(IFluentAlert alert, Exception inner) 
            : base(alert.ToText(s => s.Replace(Environment.NewLine, string.Empty)), inner)
        {
            Alert = alert;
        }
        public IFluentAlert Alert { get; set; }
    }
}
