using System;

namespace FluentAlerts
{
    public interface IFluentAlerts
    {
        IFluentAlertsBuilder Create();
        IFluentAlertsBuilder Create(string title);
        IFluentAlertsBuilder Create(object obj);
    }
}