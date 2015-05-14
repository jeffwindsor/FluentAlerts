using System;
using System.Text;
using FluentAlerts.Domain;

namespace FluentAlerts
{
    public class Html : FluentAlertSerializerTemplate
    {
        public Html()
        {
            //Document 
            SerializeTypeAsEnumerable<Document>("<div class='alert-document'>", "</div>");
            SerializeTypeWith<HorizontalRule>("<HR>", "");
            
            //Table
            SerializeTypeAsEnumerable<Table>("<table class='alert-table' cellspacing='1' cellpadding='2' width='100%'>","</table>",
                table =>
                {
                    table.ProcessTableIndexes();
                    return table;
                });
            SerializeTypeAsEnumerable<Row>("<TR>", "</TR>");
            SerializeTypeWith<Cell>(source => GetPrefix(source, "<TD class='value-normal'{0}>"), source => "</TD>", (source) => new[] { source.Content });
            SerializeTypeWith<EmphasizedCell>(source => GetPrefix(source, "<TD class='value-emphasized'{0}>"), source => "</TD>", (source) => new[] { source.Content });
            SerializeTypeWith<HeaderCell>(source => GetPrefix(source, "<TH class='value-emphasized'{0}>"), source => "</TH>", (source) => new[] { source.Content });

            //Code Block -- Insert pure code, if we inner serialize the code string the string binding will apply substitutions
            SerializeTypeWith<CodeBlock>("<pre><code>", "</code></pre>", (source, result) => result.Append(source.Code));
            SerializeTypeWith<Link>((source, result) => result.AppendFormat("<a href='{0}'>{1}</a>", source.Url, source.Text));
            SerializeTypeAsEnumerable<OrderedList>("<ol>", "</ol>");
            SerializeTypeAsEnumerable<UnOrderedList>("<ul>", "</ul>");
            SerializeTypeWith<ListItem>("<li>", "</li>", item => new[] {item.Content});

            SerializeTypeAsEnumerable<TextBlock>("<p>", "</p>");
            SerializeTypeAsEnumerable<HeaderTextBlock>(source => string.Format("<H{0}>",source.Level), source => string.Format("</H{0}>",source.Level));
            SerializeTypeWith<Text>("", "", item => new[] { item.Content });
            SerializeTypeWith<Italic>("<i>", "</i>", item => new[] { item.Content });
            SerializeTypeWith<Underscore>("<ins>", "</ins>", item => new[] { item.Content });
            SerializeTypeWith<Bold>("<b>", "</b>", item => new[] { item.Content });
            SerializeTypeWith<StrikeThrough>("<del>", "</del>", item => new[] { item.Content });
            SerializeTypeWith<NewLine>("<BR>", "");

            SerializeTypeWith<Guid>(SerializeAsToString);
            SerializeTypeWith<DateTime>(SerializeAsToString);
            SerializeTypeWith<string>((source, result) => result.Append(ApplySubstitutions(source)));

            //Matches
            SerializeMatchWith(MatchAnything, SerializeAsPropertyNameValueTable);   // Default rule, object to Public Property NameValue Table         
            SerializeMatchWith(MatchEnumsAndPrimitives, SerializeAsToString);  // Primitive and Enums
            SerializeMatchWith(MatchExceptions, SerializeAsTypeName);
        }

        public override void PreSerializationHook(StringBuilder results)
        {
            //Add Styles
            results.Append(@"<style type='text/css'> table.alert-table { font-family:Arial,Sans-Serif; font-size:8pt; background-color: #FFFFFF; } th.value-emphasized{ background-color: #E0E0E0; font-weight:bold; font-size:10pt; } td.value-title{ background-color: #A0A0A0; font-size:10pt; font-weight:bold; } td.value-emphasized{ background-color: #D0D0D0; font-weight:bold; } td.value-normal { background-color: #F0F0F0; } td#first-column.value-normal{ background-color: #E0E0E0; } </style>");
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