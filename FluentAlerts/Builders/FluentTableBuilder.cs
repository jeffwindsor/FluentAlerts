using System.Collections.Generic;
using FluentAlerts.Domain;

namespace FluentAlerts
{
    public class FluentTableBuilder : IAlertable
    {
        private readonly List<Row> _rows = new List<Row>();
        
        public FluentTableBuilder WithRow(params object[] cells)
        {
            return With(new Row(cells));
        }

        public FluentTableBuilder WithEmphasizedRow(params object[] cells)
        {
            return With(new EmphasizedRow(cells));
        }

        public FluentTableBuilder WithHeaderRow(params object[] cells)
        {
            return With(new HeaderRow(cells));
        }
        
        private FluentTableBuilder With<T>(T item) where T: Row
        {
            _rows.Add(item);
            return this;
        }
        public Table ToTable()
        {
            return new Table(_rows);
        }
        public object ToAlert()
        {
            return ToTable();
        }
    }
}