using System.Collections.Generic;
using System.Linq;

namespace FluentAlerts.Domain
{
    public class Table : List<Row>
    {
        public Table(params Row[] items) : base(items) { }
        public Table(IEnumerable<Row> items) : base(items) { }

        public void ProcessTableIndexes()
        {
            //Set cell values based on current row make-up
            var maxTableColumnNumber = (uint)this.Max(r => r.Count);
            foreach (var row in this)
            {
                var maxRowColumnNumber = (uint)row.Count;
                for (var i = 0; i < maxRowColumnNumber; i++)
                {
                    row[i].MaxTableColumnNumber = maxTableColumnNumber;
                    row[i].MaxRowColumnNumber = maxRowColumnNumber;
                    row[i].ColumnNumber = (uint)i + 1;
                }
            }
        }
    }

    public class Row : List<Cell>
    {
        public Row(params Cell[] items) : base(items) { }
        public Row(IEnumerable<Cell> items) : base(items) { }
    }

    public class Cell : ObjectContainer
    {
        public uint MaxTableColumnNumber { get; set; }
        public uint MaxRowColumnNumber { get; set; }
        public uint ColumnNumber { get; set; }
        
    }
    public class EmphasizedCell : Cell { }
    public class HeaderCell : Cell { }
}