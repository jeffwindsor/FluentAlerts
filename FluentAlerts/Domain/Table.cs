using System.Collections.Generic;

namespace FluentAlerts.Domain
{
    public class Table : List<Row>
    {
        public Table(params Row[] items) : base(items) { }
        public Table(IEnumerable<Row> items) : base(items) { }
    }

    public class Row : List<object>
    {
        public Row(params object[] items) : base(items) { }
        public Row(IEnumerable<object> items) : base(items) { }
    }

    public class EmphasizedRow : Row
    {
        public EmphasizedRow(params object[] items) : base(items) { }
        public EmphasizedRow(IEnumerable<object> items) : base(items) { }
    }

    public class HeaderRow : Row
    {
        public HeaderRow(params object[] items) : base(items) { }
        public HeaderRow(IEnumerable<object> items) : base(items) { }
    }
}