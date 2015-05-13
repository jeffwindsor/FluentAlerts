using System.Collections.Generic;
using FluentAlerts.Domain;
using FluentAlerts.Extensions;

namespace FluentAlerts.Builders
{
    public class FluentTableBuilder
    {
        private readonly List<Row> _rows = new List<Row>();
        
        public FluentTableBuilder WithRow(params object[] cells)
        {
            return With(new Row(cells.ToCellItems()));
        }

        public FluentTableBuilder WithEmphasizedRow(params object[] cells)
        {
            return With(new Row(cells.ToEmphasizedCellItems()));
        }

        public FluentTableBuilder WithHeaderRow(params object[] cells)
        {
            return With(new Row(cells.ToHeaderCellItems()));
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