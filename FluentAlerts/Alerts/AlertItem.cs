using System.Collections.Generic;

namespace FluentAlerts
{
    public abstract class AlertItem: IAlertItem 
    {
        internal ItemStyle ItemStyle { get; set; }
        internal IList<object> Values { get; set; }
    }
}
