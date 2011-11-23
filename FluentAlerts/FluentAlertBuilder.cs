using System;
using System.Collections.Generic;
using FluentAlerts.Extensions;
using FluentAlerts.Nodes;

namespace FluentAlerts
{
    //TODO: When creating the tables allow the option to "ask" for a serialization of depth.
    //      but this will require the serializers to be smart enough to do the conversion to alerts at depth later
    //      also will need a serialize ALL
    //      also will need to allow the "asking" for a limit of property names

    public interface IFluentAlertBuilder
    {
        IFluentAlertBuilder AddSeperator();
        IFluentAlertBuilder AddTextBlock(string text, TextStyle style = TextStyle.Normal);
        IFluentAlertBuilder AddUrl(string text, string url);
        IFluentAlertBuilder Add(IFluentAlert source);
        IFluentAlertBuilder AddRange(IEnumerable<IFluentAlert> source);
        IFluentAlertBuilder AddException(Exception ex, IEnumerable<string> limitToPropertyNames = null);
        IFluentAlertBuilder AddObjectsAsTables<T>(IEnumerable<T> source, int serializeToDepth = 0, IEnumerable<string> limitToPropertyNames = null) where T : class;
        IFluentAlertTableBuilder AddTable();
        IFluentAlertTableBuilder AddTable(string title);
        IFluentAlertTableBuilder AddObjectAsTable<T>(T source, int serializeToDepth = 0, IEnumerable<string> limitToPropertyNames = null) where T: class;
        IFluentAlertTableBuilder AddExceptionAsTable(Exception ex, IEnumerable<string> limitToPropertyNames = null, bool includeInnerExceptions = true);
        IFluentAlert ToAlert();
    }

    internal class FluentAlertBuilder : IFluentAlertBuilder
    {
        private readonly CompositeFluentAlert _alerts = new CompositeFluentAlert();

        public IFluentAlertBuilder AddSeperator()
        {
            return Add(new FluentAlertSeperator());
        }

        public IFluentAlertBuilder AddTextBlock(string text, TextStyle style = TextStyle.Normal)
        {
            return Add(new FluentAlertTextBlock(text, style));
        }

        public IFluentAlertBuilder AddUrl(string text, string url)
        {
            return Add(new FluentAlertUrl(text, url));
        }

        public IFluentAlertBuilder Add(IFluentAlert source)
        {
            if (source != null) _alerts.Add(source);
            return this;
        }

        public IFluentAlertBuilder AddRange(IEnumerable<IFluentAlert> source)
        {
            if (source != null) _alerts.AddRange(source);
            return this;
        }

        /// <summary>
        /// Create a list of tables from each exception in the tree and add them to list in order
        /// </summary>
        public IFluentAlertBuilder AddException(Exception ex, IEnumerable<string> limitToPropertyNames)
        {
            if (ex == null) return this;
            foreach (var exception in ex.ToList())
            {
                AddExceptionAsTable(exception, limitToPropertyNames, false);
            }
            return this;
        }

        public IFluentAlertBuilder AddObjectsAsTables<T>(IEnumerable<T> source, int serializeToDepth, IEnumerable<string> limitToPropertyNames) where T : class
        {
            if (source == null) return this;
            foreach (var item in source)
                AddObjectAsTable(item, serializeToDepth, limitToPropertyNames);

            return this;
        }

        public IFluentAlertTableBuilder AddTable()
        {
            return new FluentAlertTableBuilder(this);
        }

        public IFluentAlertTableBuilder AddTable(string title)
        {
            return AddTable()
                .AddHeaderRow(title);
        }

        /// <summary>
        /// Adds a table with the types name as the header and a row for each property
        /// </summary>
        /// <typeparam name="T">Object Type</typeparam>
        /// <param name="source">Object to add</param>
        /// <param name="serializeToDepth">How deep to serialize properties with reference type values. Zero is none.</param>
        /// <param name="limitToPropertyNames">Filter property rows to names listed</param>
        /// <returns></returns>
        public IFluentAlertTableBuilder AddObjectAsTable<T>(T source, int serializeToDepth, IEnumerable<string> limitToPropertyNames) where T : class
        {
            var result = AddTable();
            if (source == null) return result;

            return result
                    .AddHeaderRow(source.GetType().Name)
                    .AddRows(source, serializeToDepth, limitToPropertyNames);
        }

        public IFluentAlertTableBuilder AddExceptionAsTable(Exception ex, IEnumerable<string> limitToPropertyNames, bool includeInnerExceptions)
        {
            limitToPropertyNames = limitToPropertyNames ?? new HashSet<string> { "Message", "StackTrace" };

            return ScrollExceptionToTable(ex, limitToPropertyNames, includeInnerExceptions, AddTable());
        }

        public IFluentAlert ToAlert() { return _alerts; }

        /// <summary>
        /// Adds Exception tree to a single table
        /// </summary>
        private static IFluentAlertTableBuilder ScrollExceptionToTable(Exception ex, IEnumerable<string> limitToPropertyNames, bool includeInnerExceptions, IFluentAlertTableBuilder builder)
        {
            if (ex == null) return builder;

            builder
                .AddHeaderRow(ex.GetType().Name)
                .AddRows(ex, 0, limitToPropertyNames);

            return ScrollExceptionToTable(ex.InnerException, limitToPropertyNames, includeInnerExceptions, builder);
        }
    }
}
