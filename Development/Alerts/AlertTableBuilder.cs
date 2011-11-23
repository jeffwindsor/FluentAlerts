using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Alerts
{
    public interface IAlertTableBuilder: IAlertBuilder 
    {
        IAlertTableBuilder WithNumberOfColumns(int columns);
        IAlertTableBuilder AddRow(params object[] values);
        IAlertTableBuilder AddHeaderRow(params object[] values);
        IAlertTableBuilder AddFooterRow(params object[] values);
        IAlertTableBuilder AddHighlightRow(params object[] values);
        IAlertTableBuilder AddPropertyNameValuesAsRows<T>(T source, int serializeToDepth, IEnumerable<string> limitToPropertyNames = null);
    }

    internal class AlertTableBuilder : IAlertTableBuilder
    {
        private readonly AlertTable _table = new AlertTable();
        public IAlert ToAlert() { return _table; }
        public IAlertTableBuilder WithNumberOfColumns(int columns)
        {
            _table.ColumnCount = columns;
            return this;
        }
        public IAlertTableBuilder AddRow(params object[] values) { return AddTypedRow(RowStyle.Normal, values); }
        public IAlertTableBuilder AddHeaderRow(params object[] values) { return AddTypedRow(RowStyle.Header, values); }
        public IAlertTableBuilder AddFooterRow(params object[] values) { return AddTypedRow(RowStyle.Footer, values); }
        public IAlertTableBuilder AddHighlightRow(params object[] values) { return AddTypedRow(RowStyle.Highlight, values); }
        private IAlertTableBuilder AddTypedRow(RowStyle style, params object[] values)
        {
            _table.AddRow(style, values);
            return this;
        }

        public IAlertTableBuilder AddPropertyNameValuesAsRows<T>(T source, int serializeToDepth, IEnumerable<string> limitToPropertyNames = null)
        {
            //Extract Reflection Info
            var rows = from pi in source.GetType().GetProperties()
                       orderby pi.Name
                       select new
                              {
                                  pi.Name,
                                  SerializeAtThisDepth = (pi.PropertyType.IsValueType || pi.PropertyType == typeof(string)),
                                  Value = GetPropertyValue(pi, source)
                              };

            //Filter by Property Names if supplied
            if (limitToPropertyNames != null)
            {
                rows = from item in rows
                       from allowedName in limitToPropertyNames
                       where item.Name == allowedName
                       select item;
            }

            //Serialize Reflection Info to Depth
            foreach (var row in rows)
            {
                if (row.SerializeAtThisDepth)
                {
                    AddRow(row.Name, row.Value);
                }
                else if (serializeToDepth > 0)
                {
                    if (row.Value == null)
                    {
                        AddRow(row.Name, "Null");
                    }
                    else
                    {
                        var subBuilder = new AlertTableBuilder().AddHeaderRow(row.Value.GetType().Name);
                        subBuilder.AddPropertyNameValuesAsRows(row.Value, serializeToDepth - 1, limitToPropertyNames);
                        AddRow(row.Name, subBuilder.ToAlert());
                    }
                }
            }
            return this;
        }
        private static object GetPropertyValue(PropertyInfo pi, object source)
        {
            object result;
            try
            {
                result = pi.GetValue(source, null);
            }
            catch (Exception)
            {
                result = "Reflection Failed to Obtain Value";
            }
            return result;
        }
    }
}
