using System.Collections.Generic;
namespace Alerts
{
    internal class AlertTable : IAlert
    {
        private readonly List<Row> _rows = new List<Row> ();
        public AlertTable()
        {
            ColumnCount = 2;
        }
        public int ColumnCount { get; set; }
        public IEnumerable<Row> Rows { get { return _rows; } }
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
