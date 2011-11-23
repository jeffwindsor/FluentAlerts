using System.Text;

namespace Alerts
{
    internal class AlertSerializerToText : AlertSerializer
    {
        protected override void AppendSerialization(AlertSeperator source, StringBuilder sb)
        {
            sb.AppendLine();
            sb.AppendLine("================================================");
        }

        protected override void AppendSerialization(AlertTable source, StringBuilder sb)
        {
            foreach (var row in source.Rows)
            {
                AppendSerialization(row, source.ColumnCount, sb);
            }
        }

        protected override void AppendSerialization(AlertTable.Row source, int columns, StringBuilder sb)
        {
            //UNDONE: inserts 1 to many commas
            foreach(var v in source.Values)
            {
                sb.Append(v).AppendLine(", ");
            }
        }

        protected override void AppendSerialization(AlertTextBlock source, StringBuilder sb)
        {
            sb.AppendLine(source.Text.ToString());
        }

        protected override void AppendSerialization(AlertURL source, StringBuilder sb)
        {
            sb.AppendLine(string.Format("{0}<{1}>", source.Text, source.Url));
        }
        
    }

}
