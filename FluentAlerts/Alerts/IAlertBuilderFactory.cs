using System;

namespace FluentAlerts
{
    public interface IAlertBuilderFactory
    {
        IAlertBuilder Create();
        IAlertBuilder Create(string title);
        IAlertBuilder Create(object obj);
    }
}