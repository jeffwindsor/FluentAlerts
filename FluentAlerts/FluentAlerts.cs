using FluentAlerts.Builders;
using FluentAlerts.Domain;

namespace FluentAlerts
{
    public class FluentAlerts : IFluentAlerts
    {
        public FluentDocumentBuilder Document()
        {
            return new FluentDocumentBuilder(this);
        }

        public FluentDocumentBuilder Document(string header)
        {
            return Document().WithHeader(header);
        }

        public FluentTableBuilder Table()
        {
            return new FluentTableBuilder();
        }

        public FluentTableBuilder Table(string title)
        {
            return Table().WithHeaderRow(title);
        }

        public FluentTextBlockBuilder TextBlock()
        {
            return new FluentTextBlockBuilder();
        }

        public FluentTextBlockBuilder TextBlock(string text)
        {
            return TextBlock().WithNormal(text);
        }

        public FluentTextBlockBuilder HeaderTextBlock(uint level)
        {
            return new FluentTextBlockBuilder(level);
        }

        public FluentTextBlockBuilder HeaderTextBlock(string text, uint level)
        {
            return HeaderTextBlock(level).WithNormal(text);
        }

        public OrderedList OrderedList(params object[] items)
        {
            return new OrderedList(items.ToListItems());
        }

        public UnOrderedList UnOrderedList(params object[] items)
        {
            return new UnOrderedList(items.ToListItems());
        }

        public CodeBlock CodeBlock(string language, string code)
        {
            return new CodeBlock { Code = code, Language = language };
        }

        public Link Link(string url, string text = "")
        {
            return new Link
            {
                Url = url,
                Text = text
            };
        }


        public IFluentAlertSerializer HtmlSerializer()
        {
            return new FluentAlertHtmlSerializer();
        }
    }
}
