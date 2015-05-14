using FluentAlerts.Builders;
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
        FluentTextBlockBuilder HeaderTextBlock(int level);
        FluentTextBlockBuilder HeaderTextBlock(string text, int level);

        OrderedList OrderedList(params object[] items);
        UnOrderedList UnOrderedList(params object[] items);
        CodeBlock CodeBlock(string language, string code);
        Link Link(string url, string text = "");

        IFluentAlertSerializer Serializer<TTemplate>() where TTemplate : FluentAlertSerializerTemplate, new();
    }
}