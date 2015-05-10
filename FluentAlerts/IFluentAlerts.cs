using System;

namespace FluentAlerts
{
    public interface IFluentAlerts
    {
        IFluentAlertBuilder Create();
        IFluentAlertBuilder Create(string title);
        IFluentAlertBuilder Create(object obj);
    }
}