using System.Collections.Generic;

namespace FluentAlerts
{
    public interface IAlertBuilder
    {
        IAlertBuilder With(params object[] values);
        IAlertBuilder With(IEnumerable<object[]> listOfValues);
        IAlertBuilder WithEmphasized(params object[] values);
        IAlertBuilder WithSeperator();
        IAlertBuilder WithUrl(string text, string url);
        IAlertBuilder WithAlert(IAlert n);
        IAlertBuilder WithAlert(IAlertBuilder n);
        IAlertBuilder Merge(IAlert n);
        IAlertBuilder Merge(IAlertBuilder n);
        IAlertBuilder WithTitle(string  format, params object[] args);

        IAlert ToAlert();
    }
}