using System.Collections.Generic;


namespace FluentAlerts
{
    public interface IAlert: IList<IAlertItem>, IAlertItem
    {
        string Title { get; }
    }
}