using System.Collections.Generic;


namespace FluentAlerts
{
    public interface IFluentAlertBuilder
    {
        IFluentAlert ToAlert();

        IFluentAlertBuilder WithHeader(string text, uint level = 1);

        IFluentAlertBuilder WithLink(string url, string text = "");

        //IFluentAlertBuilder WithOrderedList(params object[] values);
        //IFluentAlertBuilder WithUnOrderedList(params object[] values);
        //IFluentAlertBuilder WithCodeBlock(string language, string code);
        IFluentAlertBuilder WithSeperator();


        //IFluentAlertBuilder With(params object[] values);
        //IFluentAlertBuilder With(IEnumerable<object[]> listOfValues);
        //IFluentAlertBuilder WithEmphasized(params object[] values);
        //IFluentAlertBuilder WithSeperator();
        //IFluentAlertBuilder WithUrl(string text, string url);
        //IFluentAlertBuilder WithAlert(IFluentAlert n);
        //IFluentAlertBuilder WithAlert(IFluentAlertBuilder n);
        //IFluentAlertBuilder Merge(IFluentAlert n);
        //IFluentAlertBuilder Merge(IFluentAlertBuilder n);
        //IFluentAlertBuilder WithTitle(string  format, params object[] args);

        
    }
}