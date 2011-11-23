using System;
using System.Collections.Generic;

namespace Alerts
{
    public interface IAlertBuilderFactory
    {
        IAlertTableBuilder CreateTable();
        IAlertTableBuilder CreateTable(string title);
        IAlertTableBuilder CreateTable<T>(T source, int serializeToDepth, IEnumerable<string> limitToPropertyNames = null);
        IAlertTableBuilder CreateTable(Exception ex, bool includeInnerExceptions = false, bool includeMessage = true, bool includeStackTrace = true);

        IAlertDocumentBuilder CreateDocument();
        IAlertDocumentBuilder CreateDocument(string title);
    }

    public class AlertBuilderFactory : IAlertBuilderFactory 
    {
        public IAlertTableBuilder CreateTable() { return new AlertTableBuilder();}
        public IAlertTableBuilder CreateTable(string title) { return CreateTable().AddHeaderRow(title); }
        public IAlertTableBuilder CreateTable<T>(T source, int serializeToDepth, IEnumerable<string> limitToPropertyNames = null)
        {
            //Convenience method which creates a table builder pre-filled with source's reflected properties to depth
            return new AlertTableBuilder()
                .AddHeaderRow(source.GetType().Name)
                .AddPropertyNameValuesAsRows(source, serializeToDepth, limitToPropertyNames);
        }
        public IAlertTableBuilder CreateTable(Exception ex, bool includeInnerExceptions = false, bool includeMessage = true, bool includeStackTrace = true)
        {
            //Convenience method for Exception Special Case, depth to 0 for no inner exceptions, int max for all
            var serializeToDepth = (includeInnerExceptions) ? int.MaxValue : 0;
            return CreateTable(ex, serializeToDepth, GetExceptionPropertyFilters(includeMessage, includeStackTrace));
        }
        
        public IAlertDocumentBuilder CreateDocument()
        {
            return new AlertDocumentBuilder(this);
        }

        public IAlertDocumentBuilder CreateDocument(string title)
        {
            return new AlertDocumentBuilder(this)
                .AddTextBlock(title);
        }
        
        private static IEnumerable<string> GetExceptionPropertyFilters(bool includeMessage, bool includeStackTrace)
        {
            var filter = new List<string>();
            if (includeMessage) filter.Add("Message");
            if (includeStackTrace) filter.Add("StackTrace");
            filter.Add("InnerException");
            return filter;
        }

    }
}
