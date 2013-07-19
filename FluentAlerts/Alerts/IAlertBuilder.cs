using System.Collections.Generic;

namespace FluentAlerts
{
    public interface IAlertBuilder
    {
        /// <summary>
        /// Appends text to first item if it is a text block, otherwise it inserts a text block at
        /// the first position
        /// </summary>
        IAlertBuilder WithSeperator();
        IAlertBuilder With(string text);
        IAlertBuilder With(string format, params object[] args);
        IAlertBuilder WithEmphasized(string text);
        IAlertBuilder WithEmphasized(string format, params object[] args);
        IAlertBuilder WithHeaderOne(string text);
        IAlertBuilder WithHeaderOne(string format, params object[] args);
        IAlertBuilder WithUrl(string text, string url);
        IAlertBuilder WithValue(object value);
        IAlertBuilder WithValues(IEnumerable<object> values);
        IAlertBuilder WithRow(params object[] values);
        IAlertBuilder WithEmphasizedRow(params object[] values);
        IAlertBuilder WithRows(IEnumerable <object[]> listOfValues);
        IAlertBuilder WithAlert(IAlert n);
  
        /// <summary>
        /// The build function, produces a alert with the current items
        /// </summary> 
        IAlert ToAlert();
    }
}