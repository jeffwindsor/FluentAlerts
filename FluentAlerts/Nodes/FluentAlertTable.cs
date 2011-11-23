using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FluentAlerts.Nodes
{
    internal class FluentAlertTable : IFluentAlert
    {
        private readonly List<Row> _rows = new List<Row> ();
        public FluentAlertTable()
        {
            ColumnCount = 2;
        }
        public int ColumnCount { get; set; }
        public ReadOnlyCollection<Row> Rows { get { return _rows.AsReadOnly(); } }
        public void AddRow(RowStyle style, params object[] values)
        {
            _rows.Add(new Row {Style = style, Values = values});
        }

        #region Row
        /// <summary>
        /// A row in a table where the values represent the content of "cells"
        /// </summary>
        internal class Row
        {
            public RowStyle Style { get; set; }
            public object[] Values { get; set; }
        }
        #endregion      
    }
}
