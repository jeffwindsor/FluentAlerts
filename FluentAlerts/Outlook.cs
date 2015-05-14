﻿using System.Text;
using FluentAlerts.Domain;

namespace FluentAlerts
{
    public class Outlook : FluentAlertSerializerTemplate
    {
        public Outlook()
        {
            //Document 
            SerializeTypeAsEnumerable<Document>("<div class='alert-document'>", "</div>");
            SerializeTypeWith<HorizontalRule>("<HR>");
            
            //Table
            SerializeTypeAsEnumerable<Table>("<table cellpadding='0' style='border-style:solid;border-width:1px;border-color:black;font-family:Arial,Sans-Serif;font-size:9pt;' width='100%'>",
                "</table>",
                table =>
                {
                    table.ProcessTableIndexes();
                    return table;
                });
            SerializeTypeAsEnumerable<Row>("<TR>", "</TR>");
            SerializeTypeWith<Cell>(source => GetPrefix(source, "<TD style='background-color:whitesmoke;'{0}>"), source => "</TD>", source => new[] { source.Content });
            SerializeTypeWith<EmphasizedCell>(source => GetPrefix(source, "<TD style='background-color:silver;font-weight:bold;'{0}>"), source => "</TD>", source => new[] { source.Content });
            SerializeTypeWith<HeaderCell>(source => GetPrefix(source, "<TH style='background-color:silver;font-weight:bold;font-size:10pt;'{0}>"), source => "</TH>", source => new[] { source.Content });

            //Code Block -- Insert pure code, if we inner serialize the code string the string binding will apply substitutions
            SerializeTypeWith<CodeBlock>("<pre><code>", "</code></pre>", (source, result) => result.Append(source.Code));
            SerializeTypeWith<Link>((source, result) => result.AppendFormat("<a href='{0}'>{1}</a>", source.Url, source.Text));
            SerializeTypeAsEnumerable<OrderedList>("<ol>", "</ol>");
            SerializeTypeAsEnumerable<UnOrderedList>("<ul>", "</ul>");
            SerializeTypeWith<OrderedListItem>("<li>", "</li>", item => new[] {item.Content});
            SerializeTypeWith<UnOrderedListItem>("<li>", "</li>", item => new[] { item.Content });

            SerializeTypeAsEnumerable<TextBlock>("<p>", "</p>");
            SerializeTypeAsEnumerable<HeaderTextBlock>(source => string.Format("<H{0}>",source.Level), source => string.Format("</H{0}>",source.Level));
            
            SerializeTypeWith<Text>("", "", item => new[] { item.Content });
            SerializeTypeWith<Italic>("<i>", "</i>", item => new[] { item.Content });
            SerializeTypeWith<Underscore>("<ins>", "</ins>", item => new[] { item.Content });
            SerializeTypeWith<Bold>("<b>", "</b>", item => new[] { item.Content });
            SerializeTypeWith<StrikeThrough>("<del>", "</del>", item => new[] { item.Content });
            SerializeTypeWith<NewLine>("<BR>");

            SerializeTypeWith<string>((source, result) => result.Append(ApplySubstitutions(source)));
        }

        public override void PreSerializationHook(StringBuilder results)
        {
            //Add Styles
            results.Append(@"<html><body bgcolor='white' style='color:black;padding-top:0px;padding-bottom:0px;margin-top:0px;margin-bottom:0px;'>");
        }

        public override void PostSerializationHook(StringBuilder result)
        {
            result.Append("</body></html>");
        }

        private static string GetPrefix(Cell cell, string format)
        {
            return string.Format(format, GetDecorations(cell));
        }
        private static string GetDecorations(Cell cell)
        {
            var decorations = new StringBuilder();
            //SPAN
            if (cell.CellNumber == cell.Row.Count)
            {
                var spanColumns = 1 + cell.Row.Table.Columns - cell.CellNumber;
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