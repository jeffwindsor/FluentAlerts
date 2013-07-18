using System.Collections.Generic;

namespace FluentAlerts
{
    public interface IAlertFactory
    {
        /// <summary>
        /// Create an alert from a style and list of alert items (in order)
        /// </summary>
        /// <returns></returns>
        IAlert Create(IList<IAlertItem> items);
    }
}