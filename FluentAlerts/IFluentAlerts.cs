using FluentAlerts.Domain;

namespace FluentAlerts
{
    public interface IFluentAlerts
    {
        FluentDocumentBuilder Document();
        FluentDocumentBuilder Document(string header);
        FluentTableBuilder Table();
        FluentTableBuilder Table(string title);
        FluentTextBlockBuilder TextBlock();
        FluentTextBlockBuilder TextBlock(string text);
        FluentTextBlockBuilder HeaderTextBlock(uint level);
        FluentTextBlockBuilder HeaderTextBlock(string text, uint level);

        Document From(object obj);
        OrderedList OrderedList(params object[] items);
        UnOrderedList UnOrderedList(params object[] items);
        CodeBlock CodeBlock(string language, string code);
        Link Link(string url, string text = "");
    }
}