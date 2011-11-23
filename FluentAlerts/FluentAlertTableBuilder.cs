using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentAlerts.Nodes;

namespace FluentAlerts
{
    public interface IFluentAlertTableBuilder
    {
        IFluentAlertTableBuilder WithNumberOfColumns(int columns);
        IFluentAlertTableBuilder AddRow(params object[] values);
        IFluentAlertTableBuilder AddHeaderRow(params object[] values);
        IFluentAlertTableBuilder AddFooterRow(params object[] values);
        IFluentAlertTableBuilder AddHighlightedRow(params object[] values);
        IFluentAlertTableBuilder AddRows<T>(T source, int convertRefTypesToDepth, IEnumerable<string> limitToPropertyNames = null);
        IFluentAlertBuilder Close();
    }

    internal class FluentAlertTableBuilder : IFluentAlertTableBuilder
    {
        private readonly FluentAlertTable _table = new FluentAlertTable();
        private readonly IFluentAlertBuilder _parent;
        public FluentAlertTableBuilder(IFluentAlertBuilder parent)
        {
            //Store Parent for close and add the table being built
            _parent = parent;
            parent.Add(_table);
        }

        public IFluentAlertTableBuilder WithNumberOfColumns(int columns)
        {
            _table.ColumnCount = columns;
            return this;
        }
        public IFluentAlertTableBuilder AddRow(params object[] values) { return AddTypedRow(RowStyle.Normal, values); }
        public IFluentAlertTableBuilder AddHeaderRow(params object[] values) { return AddTypedRow(RowStyle.Header, values); }
        public IFluentAlertTableBuilder AddFooterRow(params object[] values) { return AddTypedRow(RowStyle.Footer, values); }
        public IFluentAlertTableBuilder AddHighlightedRow(params object[] values) { return AddTypedRow(RowStyle.Highlighted, values); }

        private IFluentAlertTableBuilder AddTypedRow(RowStyle style, params object[] values)
        {
            _table.AddRow(style, values);
            return this;
        }

        public IFluentAlertTableBuilder AddRows<T>(T source, int convertRefTypesToDepth, IEnumerable<string> limitToPropertyNames = null)
        {
            //Get Property Name Value Pairs
            var pairs = GetPropertyNameValuePairs(source, limitToPropertyNames);

            //Add Pairs as Rows
            foreach (var row in pairs)
            {
                if (convertRefTypesToDepth == 0 || row.Value is string || row.Value is ValueType)
                {
                    //Create Row from Name and Value
                    AddRow(row.Name, row.Value);
                }
                else if (convertRefTypesToDepth > 0)
                {
                    //Create Row from Name and New Alert Table for Value
                    AddRow(row.Name, new FluentAlertTableBuilder(_parent)
                                         .AddHeaderRow(row.Value.GetType().Name)
                                         .AddRows(row.Value, convertRefTypesToDepth - 1, limitToPropertyNames)
                                         .Close()
                                         );
                }
            }
            return this;
        }

        public IFluentAlertTableBuilder AddPropertyValuesAsRow<T>(T source, IEnumerable<string> limitToPropertyNames = null)
        {
            //Get Property Name Value Pairs
            var values = from item in GetPropertyNameValuePairs(source, limitToPropertyNames)
                         select item.Value;
            AddRow(values.ToArray());
            return this;
        }

        public IFluentAlertTableBuilder AddPropertyNamesAsRow<T>(IEnumerable<string> limitToPropertyNames = null)
        {
            var names = GetPropertyNames<T>(limitToPropertyNames).Cast<object>().ToArray();
            AddRow(names);
            return this;
        }

        public IFluentAlertBuilder Close()
        {
            return _parent;
        }

        private static IEnumerable<string> GetPropertyNames<T>(IEnumerable<string> limitToPropertyNames = null)
        {
            return limitToPropertyNames ??
                   from pi in typeof(T).GetProperties()
                   select pi.Name;
        }

        private static IEnumerable<NameValue> GetPropertyNameValuePairs<T>(T source, IEnumerable<string> limitToPropertyNames = null)
        {
            //Extract Reflection Info
            var pairs = from pi in source.GetType().GetProperties()
                        orderby pi.Name
                        select new NameValue { Name = pi.Name, Value = GetPropertyValue(pi, source) };

            //Filter by Property Names if supplied
            if (limitToPropertyNames != null)
            {
                pairs = from item in pairs
                        from allowedName in limitToPropertyNames
                        where item.Name == allowedName
                        select item;
            }

            return pairs;
        }

        private static object GetPropertyValue(PropertyInfo pi, object source)
        {
            try
            {
                var result = pi.GetValue(source, null);
                return result ?? "Null";
            }
            catch (Exception)
            {
                return "Reflection Failed to Obtain Value";
            }
        }

        //Using this instead of tuple just for readability
        private class NameValue
        {
            public string Name { get; set; }
            public object Value { get; set; }
        }
    }
}
