using System.Collections.Generic;
using System.Linq;

namespace FluentAlerts.Domain
{
    public class Table : List<Row>
    {
        public int Columns { get; set; }
        public Table(params Row[] items) : base(items) { }
        public Table(IEnumerable<Row> items) : base(items) { }

        public void ProcessTableIndexes()
        {
            if (!this.Any()) return;
            
            //Set cell values based on current row make-up
            Columns = this.Max(r => r.Count);
            for(var rowIndex = 0; rowIndex < Count; rowIndex++)
            {
                var row = this[rowIndex];
                row.Table = this;
                row.RowNumber = rowIndex + 1;
                for (var cellIndex = 0; cellIndex < row.Count; cellIndex++)
                {
                    var cell = row[cellIndex];
                    cell.Row = row;
                    cell.CellNumber = cellIndex + 1;
                }
            }
        }
    }

    public class Row : List<Cell>
    {
        public Row(params Cell[] items) : base(items) { }
        public Row(IEnumerable<Cell> items) : base(items) { }
        
        public Table Table { get; set; }
        public int Columns { get { return Count; } }
        public int RowNumber { get; set; }
    }

    public class Cell
    {
        public Row Row { get; set; }
        public object Content { get; set; }
        public int CellNumber { get; set; }
        
    }
    public class EmphasizedCell : Cell { }
    public class HeaderCell : Cell { }
}