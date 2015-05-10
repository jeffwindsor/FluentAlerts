using System;

namespace FluentAlerts
{
    public interface IAlertable
    {
        Alert ToAlert();
    }

    public interface IFluentAlerts
    {
        FluentDocumentBuilder Document();
        FluentDocumentBuilder Document(string header);
        FluentDocumentBuilder Document(object obj);
        FluentTableBuilder Table();
        FluentTableBuilder Table(string title);
        FluentTextBlockBuilder TextBlock();
        FluentTextBlockBuilder TextBlock(string text);
        //Simple Documents
        Alert Link(string url, string text = "");
        Alert OrderedList(params object[] items);
        Alert UnOrderedList(params object[] items);
        Alert CodeBlock(string language, string code);
        Alert ObjectBlock(object obj);
        Alert ExceptionBlock(Exception ex);
    }
}