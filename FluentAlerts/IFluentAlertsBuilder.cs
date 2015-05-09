using System.Collections.Generic;


namespace FluentAlerts
{
    public interface IFluentAlertsBuilder
    {
        IFluentAlertsBuilder With(params object[] values);
        IFluentAlertsBuilder With(IEnumerable<object[]> listOfValues);
        IFluentAlertsBuilder WithEmphasized(params object[] values);
        IFluentAlertsBuilder WithSeperator();
        IFluentAlertsBuilder WithUrl(string text, string url);
        IFluentAlertsBuilder WithAlert(IAlert n);
        IFluentAlertsBuilder WithAlert(IFluentAlertsBuilder n);
        IFluentAlertsBuilder Merge(IAlert n);
        IFluentAlertsBuilder Merge(IFluentAlertsBuilder n);
        IFluentAlertsBuilder WithTitle(string  format, params object[] args);

        IAlert ToAlert();
    }
}