using System.Collections.Generic;


namespace FluentAlerts
{
    public interface IFluentAlertBuilder
    {
        IFluentAlertBuilder With(params object[] values);
        IFluentAlertBuilder With(IEnumerable<object[]> listOfValues);
        IFluentAlertBuilder WithEmphasized(params object[] values);
        IFluentAlertBuilder WithSeperator();
        IFluentAlertBuilder WithUrl(string text, string url);
        IFluentAlertBuilder WithAlert(IFluentAlert n);
        IFluentAlertBuilder WithAlert(IFluentAlertBuilder n);
        IFluentAlertBuilder Merge(IFluentAlert n);
        IFluentAlertBuilder Merge(IFluentAlertBuilder n);
        IFluentAlertBuilder WithTitle(string  format, params object[] args);

        IFluentAlert ToAlert();
    }
}