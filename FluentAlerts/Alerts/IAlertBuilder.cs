using System.Collections.Generic;

namespace FluentAlerts
{
    public interface IAlertBuilder
    {
        /// <summary>
        /// Appends text to first item if it is a text block, otherwise it inserts a text block at
        /// the first position
        /// </summary>
        IAlertBuilder With(params object[] values);
        IAlertBuilder WithEmphasized(params object[] values);
        IAlertBuilder WithSeperator();
        IAlertBuilder WithUrl(string text, string url);
        IAlertBuilder WithAlert(IAlert n);
        IAlertBuilder WithText(string format, params object[] args);
        IAlertBuilder WithEmphasizedText(string format, params object[] args);
        IAlertBuilder WithTitle(string format, params object[] args);
        //IAlertBuilder WithValues(IEnumerable<object> values);
        //IAlertBuilder WithRows(IEnumerable <object[]> listOfValues);
        
        /// <summary>
        /// The build function, produces a alert with the current items
        /// </summary> 
        IAlert ToAlert();
    }
}