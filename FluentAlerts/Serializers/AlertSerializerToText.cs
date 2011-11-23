using System.Text;
using System.Linq;
using FluentAlerts.Nodes;

namespace FluentAlerts.Serializers
{
    internal class AlertSerializerToText : AlertSerializer
    {
        protected override void AppendSerialization(FluentAlertSeperator source, StringBuilder sb)
        {
            sb.AppendLine();
            sb.AppendLine("================================================");
        }

        protected override void AppendSerialization(FluentAlertTable source, StringBuilder sb)
        {
            foreach (var row in source.Rows)
            {
                AppendSerialization(row, source.ColumnCount, sb);
            }
        }

        protected override void AppendSerialization(FluentAlertTable.Row source, int columns, StringBuilder sb)
        {
            sb.AppendLine(string.Join(",", source.Values.Select(v => v.ToString())));
        }

        protected override void AppendSerialization(FluentAlertTextBlock source, StringBuilder sb)
        {
            sb.AppendLine(source.Text.ToString());
        }

        protected override void AppendSerialization(FluentAlertUrl source, StringBuilder sb)
        {
            sb.AppendLine(string.Format("{0}<{1}>", source.Text, source.Url));
        }
        
    }

}
