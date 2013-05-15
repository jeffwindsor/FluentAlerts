using System.Collections.Generic;

namespace FluentAlerts
{
    public interface IAlert: IList<IAlertItem>, IAlertItem
    {
        AlertStyle Style { get; set; }
    }

    internal class Alert : List<IAlertItem>, IAlert 
    {
        public AlertStyle Style { get; set; }
    }
}
