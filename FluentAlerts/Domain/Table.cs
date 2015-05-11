using System.Collections.Generic;

namespace FluentAlerts.Domain
{
    public class Table : List<Row>
    {
        public Table(params Row[] items) : base(items) { }
        public Table(IEnumerable<Row> items) : base(items) { }
    }

    public class Row : List<Cell>
    {
        public Row(params Cell[] items) : base(items) { }
        public Row(IEnumerable<Cell> items) : base(items) { }
    }

    public class EmphasizedRow : Row
    {
        public EmphasizedRow(params Cell[] items) : base(items) { }
        public EmphasizedRow(IEnumerable<Cell> items) : base(items) { }
    }

    public class HeaderRow : Row
    {
        public HeaderRow(params Cell[] items) : base(items) { }
        public HeaderRow(IEnumerable<Cell> items) : base(items) { }
    }

    public class Cell : ObjectContainer
    {
    }
}