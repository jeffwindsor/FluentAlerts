using FluentAlerts.Domain;

namespace FluentAlerts
{
    public class GitHubMarkdown : FluentAlertSerializerTemplate
    {
        public GitHubMarkdown()
        {
            //Document 
            SerializeTypeAsEnumerable<Document>("");
            SerializeTypeWith<HorizontalRule>("***", "\r\n");
            
            ////Table
            SerializeTypeAsEnumerable<Table>("","",
                table =>
                {
                    table.ProcessTableIndexes();
                    return table;
                });
            SerializeTypeAsEnumerable<Row>("", "\r\n\r\n");
            SerializeTypeWith<Cell>("", " ", source => new[] { source.Content });
            SerializeTypeWith<EmphasizedCell>("**", "** ", source => new[] { source.Content });
            SerializeTypeWith<HeaderCell>("**_", "_** ", source => new[] { source.Content });

            SerializeTypeAsEnumerable<OrderedList>("");
            SerializeTypeAsEnumerable<UnOrderedList>("");
            SerializeTypeWith<OrderedListItem>("1. ", "", item => new[] {item.Content});
            SerializeTypeWith<UnOrderedListItem>("* ", "", item => new[] { item.Content });

            SerializeTypeAsEnumerable<TextBlock>("", "\r\n\r\n");
            SerializeTypeAsEnumerable<HeaderTextBlock>(source => new string('#', source.Level), source => "\r\n\r\n");
            SerializeTypeWith<CodeBlock>(source => string.Format("\r\n```{0}\r\n", source.Language), source => "\r\n```\r\n", (source, result) => result.Append(source.Code));
            SerializeTypeWith<Link>(source => (string.IsNullOrEmpty(source.Text)) ? "" : string.Format(" [{0}]", source.Text), source => string.Format("({0}) ", source.Url));
            
            SerializeTypeWith<Text>("", "", item => new[] { item.Content });
            SerializeTypeWith<Italic>("*", "*", item => new[] { item.Content });
            SerializeTypeWith<Underscore>("", "", item => new[] { item.Content });
            SerializeTypeWith<Bold>("**", "**", item => new[] { item.Content });
            SerializeTypeWith<StrikeThrough>("~~", "~~", item => new[] { item.Content });
            SerializeTypeWith<NewLine>("\r\n\r\n");

        }
    }
}
