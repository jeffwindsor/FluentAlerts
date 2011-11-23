using System;
using System.Collections.Generic;
using System.Linq;

namespace Alerts
{
    public interface IAlertDocumentBuilder: IAlertBuilder 
    {
        IAlertDocumentBuilder AddSeperator();
        IAlertDocumentBuilder AddTextBlock(string text, TextStyle style = TextStyle.Normal);
        IAlertDocumentBuilder AddURL(string text, string url);
        IAlertDocumentBuilder AddAlert(IAlert source);
        IAlertDocumentBuilder AddAlerts(IEnumerable<IAlert> source);
        IAlertDocumentBuilder AddExceptionAsTable(Exception ex, bool includeInnerExceptions = false, bool includeMessage = true, bool includeStackTrace = true);
        IAlertDocumentBuilder AddExceptionAsList(Exception ex, bool includeMessage = true, bool includeStackTrace = true);
        IAlertDocumentBuilder AddAsTable<T>(T source, int serializeToDepth = 0, IEnumerable<string> limitToPropertyNames = null) where T : class;
        IAlertDocumentBuilder AddAsTables<T>(IEnumerable<T> source, int serializeToDepth = 0, IEnumerable<string> limitToPropertyNames = null) where T : class;
    }
    internal class AlertDocumentBuilder : IAlertDocumentBuilder 
    {
        private readonly CompositeAlert _alerts = new CompositeAlert();
        private readonly IAlertBuilderFactory _alertBuilderfactory;
        public AlertDocumentBuilder(IAlertBuilderFactory factory)
        {
            _alertBuilderfactory = factory;
        }

        public IAlertDocumentBuilder AddAlert(IAlert source)
        {
            _alerts.Add(source);
            return this;
        }
        public IAlertDocumentBuilder AddAlerts(IEnumerable<IAlert> source)
        {
            _alerts.AddRange(source);
            return this;
        }

        public IAlert ToAlert() { return _alerts; }
        public IAlertDocumentBuilder AddSeperator() { return AddAlert(new AlertSeperator()); }
        public IAlertDocumentBuilder AddTextBlock(string text, TextStyle style = TextStyle.Normal) { return AddAlert(new AlertTextBlock(text, style));}
        public IAlertDocumentBuilder AddURL(string text, string url) { return AddAlert(new AlertURL(text, url)); }            
        public IAlertDocumentBuilder AddAsTable<T>(T source, int serializeToDepth = 0, IEnumerable<string> limitToPropertyNames = null) where T : class
        {
            return source == null ? this : AddAlert(_alertBuilderfactory.CreateTable(source, serializeToDepth, limitToPropertyNames).ToAlert());
        }
        public IAlertDocumentBuilder AddAsTables<T>(IEnumerable<T> source, int serializeToDepth = 0, IEnumerable<string> limitToPropertyNames = null) where T : class
        {
            return (source == null)
                       ? this
                       : AddAlerts(
                           source.Select(
                               item =>
                               _alertBuilderfactory.CreateTable(item, serializeToDepth, limitToPropertyNames)
                                   .ToAlert()));
        }       
        public IAlertDocumentBuilder AddExceptionAsTable(Exception ex,  bool includeInnerExceptions = false, bool includeMessage = true, bool includeStackTrace = true)
        {
            //Create a table of the first exceptions in the tree and add to the list
            return AddAlert(_alertBuilderfactory.CreateTable(ex, includeInnerExceptions, includeMessage, includeStackTrace).ToAlert());
        }
        public IAlertDocumentBuilder AddExceptionAsList(Exception ex, bool includeMessage = true, bool includeStackTrace = true)
        {           
            //Create a list of tables of each exception in the tree in order and add them to list
            return AddAlerts(from exception in ex.ToList()
                             select _alertBuilderfactory.CreateTable(exception, false, includeMessage,
                                                                            includeStackTrace).ToAlert());

        }
    }
}
