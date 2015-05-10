using System.Collections.Generic;


namespace FluentAlerts
{
    public interface IFluentAlert: IList<IFluentAlertItem>, IFluentAlertItem
    {
        string Title { get; }
    }
}