using System.Text;
using FluentAlerts.Domain;

namespace FluentAlerts.Serializers
{
    public interface IFluentAlertHtmlSerializer
    {
        string Serialize(object source);
    }
    public class FluentAlertHtmlSerializer : FluentAlertSerializer, IFluentAlertHtmlSerializer
    {
        //protected override void PreSerializationHook(object source, StringBuilder results)
        //{
        //    if (source is Document)
        //        results.Append("<div>");
        //    else if (source is Table)
        //        results.Append("<table class='alert-table' cellspacing='1' cellpadding='2' width='100%'>");
        //}
        
        //protected override void PostSerializationHook(object source, StringBuilder results)
        //{
        //    if (source is Document)
        //        results.Append("</div>");
        //    else if (source is Table)
        //        results.Append("</table>");
        //}
        public FluentAlertHtmlSerializer()
        {
            BindSerializationOf<Table>((source, results, serialize) =>
            {
                var table = (Table) source;
                results.Append("<table class='alert-table' cellspacing='1' cellpadding='2' width='100%'>");
                foreach (var row in table)
                {
                    serialize(row, results);
                }
                results.Append("</table>");
            });
            BindSerializationOf<Row>((source, results, serialize) =>
            {
                var row = (Row)source;
                results.Append("<TR>");
                foreach (var cell in row)
                {
                    serialize(cell, results);
                }
                results.Append("</TR>");
            });
            BindSerializationOf<Cell>((source, results, serialize) =>
            {
                var cell = (Cell)source;
                results.Append("<TD>");
                serialize(cell.Content, results);
                results.Append("</TD>");
            });
        }
    }
}
