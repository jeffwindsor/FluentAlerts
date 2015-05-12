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
            SerializeWith<HorizontalRule>("<HR>", "");
            
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
            SerializeWith<HeaderCell>(source => GetPrefix(source, "<TH class='value-emphasized'{0}>"), source => "</TH>", (source) => new[] { source.Content });

            //Code Block -- Insert pure code, if we inner serialize the code string the string binding will apply substitutions
            SerializeWith<CodeBlock>("<pre><code>", "</code></pre>", (source, result) => result.Append(source.Code));
            SerializeWith<Link>((source, result) => result.AppendFormat("<a href='{0}'>{1}</a>", source.Url, source.Text));
            SerializeAsListWith<OrderedList>("<ol>", "</ol>");
            SerializeAsListWith<UnOrderedList>("<ul>", "</ul>");
            SerializeWith<ListItem>("<li>", "</li>", item => new[] {item.Content});

            SerializeAsListWith<TextBlock>("<p>", "</p>");
            SerializeAsListWith<HeaderTextBlock>(source => string.Format("<H{0}>",source.Level), source => string.Format("</H{0}>",source.Level));
            SerializeWith<Text>("", "", item => new[] { item.Content });
            SerializeWith<Italic>("<i>", "</i>", item => new[] { item.Content });
            SerializeWith<Underscore>("<ins>", "</ins>", item => new[] { item.Content });
            SerializeWith<Bold>("<b>", "</b>", item => new[] { item.Content });
            SerializeWith<StrikeThrough>("<del>", "</del>", item => new[] { item.Content });
            SerializeWith<NewLine>("<BR>", "");

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
	th.value-emphasized{
		background-color: #E0E0E0;
		font-weight:bold;
        font-size:10pt;
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

            //Column ids - colored correctly if single column is assigned last column
            if (cell.ColumnNumber == cell.MaxRowColumnNumber)
                decorations.Append(" id='last-column'");
            else if (cell.ColumnNumber == 1)
                decorations.Append(" id='first-column'");

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