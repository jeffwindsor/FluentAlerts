using System.Collections.Generic;
using System.Linq;
using FluentAlerts.Domain;

namespace FluentAlerts.Builders
{
    public class FluentTableBuilder
    {
        private readonly List<Row> _rows = new List<Row>();
        
        public FluentTableBuilder WithRow(params object[] cells)
        {
            return With(new Row(cells.Select(c => new Cell { Content = c })));
        }

        public FluentTableBuilder WithEmphasizedRow(params object[] cells)
        {
            return With(new Row(cells.Select(c => new EmphasizedCell { Content = c } )));
        }

        public FluentTableBuilder WithHeaderRow(params object[] cells)
        {
            return With(new Row(cells.Select(c => new HeaderCell {Content = c})));
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
    }
}