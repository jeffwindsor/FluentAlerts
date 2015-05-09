using System.Collections.Generic;

namespace FluentAlerts
{
    public class AlertItem: IAlertItem 
    {
        internal AlertItemStyle ItemStyle { get; set; }
        internal IList<object> Values { get; set; }
    }
}
