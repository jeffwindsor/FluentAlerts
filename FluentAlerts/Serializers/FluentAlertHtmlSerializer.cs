using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
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
        public FluentAlertHtmlSerializer()
        {
            //Document 
            SerializeAsListWith<Document>("<div class='alert-document'>", "</div>");
            SerializeWith<HorizontalRule>("<div class='alert-document'>", "</div>");

            //Table
            SerializeAsListWith<Table>("<table class='alert-table' cellspacing='1' cellpadding='2' width='100%'>","</table>",
                table =>
                {
                    table.ProcessTableIndexes();
                    return table;
                });
            SerializeAsListWith<Row>("<TR>", "</TR>");
            SerializeWith<Cell>(source => GetPrefix(source, "<TD class='value-normal'{0}>"), source => "</TD>", (source) => new[] { source.Content });
            SerializeWith<EmphasizedCell>(source => GetPrefix(source, "<TD class='value-emphasized'{0}>"), source => "</TD>", (source) => new[] { source.Content });
            SerializeWith<HeaderCell>(source => GetPrefix(source, "<TH{0}>"), source => "</TH>", (source) => new[] { source.Content });

            //Other
            SerializeWith<string>((source, result, serialize) => result.Append(ApplySubstitutions(source)));
        }

        protected override void PreSerializationHook(StringBuilder results)
        {
            //Add Styles
            results.Append(@"<style type='text/css'>
	table.alert-table {
		font-family:Arial,Sans-Serif;
		font-size:8pt; 
		background-color: #FFFFFF;
		}
	td.value-title{
		background-color: #A0A0A0;
		font-size:10pt;
		font-weight:bold;
		}
	td.value-emphasized{
		background-color: #D0D0D0;
		font-weight:bold;
		}
	td.value-normal {
		background-color: #F0F0F0;
		}  
	td#first-column.value-normal{
		background-color: #E0E0E0;
		}
	</style>");
        }

        private static string GetPrefix(Cell cell, string format)
        {
            return string.Format(format, GetDecorations(cell));
        }
        private static string GetDecorations(Cell cell)
        {
            var decorations = new StringBuilder();

            //Column ids
            if (cell.ColumnNumber == 1)
                decorations.Append(" id='first-column'");
            else if (cell.ColumnNumber == cell.MaxRowColumnNumber)
                decorations.Append(" id='last-column'");

            //SPAN
            if (cell.ColumnNumber == cell.MaxRowColumnNumber)
            {
                var spanColumns = 1 + cell.MaxTableColumnNumber - cell.ColumnNumber;
                if (spanColumns > 1)
                    decorations.AppendFormat(" colspan='{0}'", spanColumns);
            }

            return decorations.ToString();
        }
        private static string ApplySubstitutions(string source)
        {
            return source.Replace("&", "&amp")
                .Replace("<", "&LT")
                .Replace(">", "&GT");
        }
    }
}

/*
 * {
	"Substitutions" :
	{
		"&" : "&amp",
		"<" : "&LT",
		">" : "&GT"
	},
	"Templates" : 
	{
		"SerializationHeader": "<style type='text/css'>
		table.alert-table {
			font-family:Arial,Sans-Serif;
			font-size:8pt; 
			background-color: #FFFFFF;
			}
		td.value-title{
			background-color: #A0A0A0;
			font-size:10pt;
			font-weight:bold;
			}
		td.value-emphasized{
			background-color: #D0D0D0;
			font-weight:bold;
			}
		td.value-normal {
			background-color: #F0F0F0;
			}  
		td#first-column.value-normal{
			background-color: #E0E0E0;
			}
		</style>",
		"AlertHeader": "<table class='alert-table' cellspacing='1' cellpadding='2' width='100%'>",
		"AlertFooter": "</table>",
		"AlertItemHeader": "<TR>",
		"AlertItemFooter": "</TR>",
		"ValueHeader": "<TD{Decorations}>",
		"ValueFooter": "</TD>",	
		"ValueSpanningDecoration": "colspan='{SpanColumns}'",
		"ValueNormalDecoration": "class='value-normal'",
		"ValueEmphasizedDecoration": "class='value-emphasized'",
		"ValueTitleDecoration": "class='value-title'",
		"ValueUrlDecoration":"class='value-url'",	
		"ValueFirstColumnId":"id='first-column'",
		"ValueLastColumnId":"id='last-column'",
		"UrlValue": "<a href='{Url}'>{UrlTitle}</a>"    
	}
}
*/