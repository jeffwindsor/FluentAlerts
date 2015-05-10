using System.Collections.Generic;

namespace FluentAlerts
{
    public class AlertItem: IFluentAlertItem 
    {
        public string Style { get; internal set; }
        public string 
        internal IList<object> Values { get; set; }
    }
}
