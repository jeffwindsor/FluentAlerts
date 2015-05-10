using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FluentAlerts
{
    public enum FluentAlertTypes
    {
        Document,
        Table,
        TextBlock,
        Link,
        Href,
        OrderedList,
        UnOrderedList,
        Code,
        ObjectBlock,
        ExceptionBlock,
        HorizontalRule,
        HeaderBlock,
        Row,
        EmphasizedRow,
        HeaderRow,
        Text,
        Italic,
        Underscore,
        Strong,
        NewLine,
        EndBlock,
        CodeText,
        CodeLanguage,
        HeaderLevel
    }

    public class Alert
    {
        public string Type { get; set; }
        public object[] Values { get; set; }
    }

    public class FluentAlertBuilder : List<Alert>, IFluentAlerts
    {
        public Alert Link(string url, string text = "")
        {
            var values = new ArrayList {HRef(url)};
            if (string.IsNullOrEmpty(text))
                Text(text);
            return CreateAlert(FluentAlertTypes.Link, values);
        }
        public Alert HRef(string value)
        {
            return CreateAlert(FluentAlertTypes.Href, value);
        }

        public Alert OrderedList(params object[] items)
        {
            return CreateAlert(FluentAlertTypes.OrderedList, items);
        }

        public Alert UnOrderedList(params object[] items)
        {
            return CreateAlert(FluentAlertTypes.UnOrderedList, items);
        }

        public Alert CodeBlock(string language, string code)
        {
            return CreateAlert(FluentAlertTypes.Code, CodeLanguage(language), CodeText(code));
        }
        public Alert CodeText(string text)
        {
            return CreateAlert(FluentAlertTypes.CodeText, text);
        }
        public Alert CodeLanguage(string text)
        {
            return CreateAlert(FluentAlertTypes.CodeLanguage, text);
        }

        public Alert ObjectBlock(object obj)
        {
            return CreateAlert(FluentAlertTypes.ObjectBlock, obj);
        }

        public Alert ExceptionBlock(Exception ex)
        {
            return CreateAlert(FluentAlertTypes.ExceptionBlock, ex);
        }

        public Alert HorizontalRule()
        {
            return CreateAlert(FluentAlertTypes.HorizontalRule);
        }

        public Alert HeaderBlock(string text, uint level)
        {
            return CreateAlert(FluentAlertTypes.HeaderBlock, Text(text), HeaderLevel(level));
        }
        public Alert HeaderLevel(uint level)
        {
            return CreateAlert(FluentAlertTypes.HeaderLevel, level);
        }

        public Alert Row(params object[] cells)
        {
            return CreateAlert(FluentAlertTypes.Row, cells);
        }

        public Alert EmphasizedRow(params object[] cells)
        {
            return CreateAlert(FluentAlertTypes.EmphasizedRow, cells);
        }

        public Alert HeaderRow(params object[] cells)
        {
            return CreateAlert(FluentAlertTypes.HeaderRow, cells);
        }

        public Alert Text(string text)
        {
            return CreateAlert(FluentAlertTypes.Text, text);
        }

        public Alert Italic(string text)
        {
            return CreateAlert(FluentAlertTypes.Italic, text);
        }

        public Alert Underscore(string text)
        {
            return CreateAlert(FluentAlertTypes.Underscore, text);
        }

        public Alert Strong(string text)
        {
            return CreateAlert(FluentAlertTypes.Strong, text);
        }

        public Alert NewLine()
        {
            return CreateAlert(FluentAlertTypes.NewLine);
        }

        private static string Convert(FluentAlertTypes type)
        {
            return type.ToString("G");
        }

        private static Alert CreateAlert(FluentAlertTypes type, params object[] values)
        {
            return new Alert
            {
                Type = Convert(type),
                Values = values
            };
        }

        public Alert ToAlert(FluentAlertTypes type)
        {
            return ToAlert(Convert(type));
        }
        public Alert ToAlert(string type)
        {
            return new Alert
            {
                Type = type,
                Values = this.Select(o=> o as object).ToArray()
            };
        }
    
        public FluentDocumentBuilder Document()
        {
            return new FluentDocumentBuilder(this);
        }

        public FluentDocumentBuilder Document(string header)
        {
            return Document().WithHeader(header);
        }

        public FluentDocumentBuilder Document(object obj)
        {
            return Document().WithObjectBlock(obj);
        }

        public FluentTableBuilder Table()
        {
            return new FluentTableBuilder(this);
        }

        public FluentTableBuilder Table(string title)
        {
            return Table().WithHeaderRow(title);
        }

        public FluentTextBlockBuilder TextBlock()
        {
            return new FluentTextBlockBuilder(this);
        }

        public FluentTextBlockBuilder TextBlock(string text)
        {
            return TextBlock().WithText(text);
        }
    }

    public class FluentDocumentBuilder: IAlertable
    {
        private readonly FluentAlertBuilder _builder;
        public FluentDocumentBuilder(FluentAlertBuilder builder)
        {
            _builder = builder;
        }

        public FluentDocumentBuilder WithHorizontalRule()
        {
            return With(_builder.HorizontalRule());
        }

        public FluentDocumentBuilder WithObjectBlock(object obj)
        {
            return With(_builder.ObjectBlock(obj));
        }

        public FluentDocumentBuilder WithExceptionBlock(Exception ex)
        {
            return With(_builder.ExceptionBlock(ex));
        }

        public FluentDocumentBuilder WithHeader(string text, uint level = 1)
        {
            return With(_builder.HeaderBlock(text,level));
        }

        public FluentDocumentBuilder WithLink(string url, string text = "")
        {
            return With(_builder.Link(url,text));
        }

        public FluentDocumentBuilder WithOrderedList(params object[] items)
        {
            return With(_builder.OrderedList(items));
        }

        public FluentDocumentBuilder WithUnOrderedList(params object[] items)
        {
            return With(_builder.UnOrderedList(items));
        }
        public FluentDocumentBuilder WithTextBlock(string text)
        {
            return With(_builder.TextBlock(text).ToAlert());
        }
        public FluentDocumentBuilder WithCodeBlock(string language, string code)
        {
            return With(_builder.CodeBlock(language, code));
        }
        public FluentDocumentBuilder With(IAlertable alertable)
        {
            return With(alertable.ToAlert());
        }
        public FluentDocumentBuilder With(Alert alert)
        {
            _builder.Add(alert);
            return this;
        }
        public Alert ToAlert()
        {
            return _builder.ToAlert(FluentAlertTypes.Document);
        }
    }

    public class FluentTableBuilder : IAlertable
    {
        private readonly FluentAlertBuilder _builder;
        public FluentTableBuilder(FluentAlertBuilder builder)
        {
            _builder = builder;
        }
        public FluentTableBuilder WithRow(params object[] cells)
        {
            return With(_builder.Row(cells));
        }

        public FluentTableBuilder WithEmphasizedRow(params object[] cells)
        {
            return With(_builder.EmphasizedRow(cells));
        }

        public FluentTableBuilder WithHeaderRow(params object[] cells)
        {
            return With(_builder.HeaderRow(cells));
        }

        private FluentTableBuilder With(Alert alert)
        {
            _builder.Add(alert);
            return this;
        }
        public Alert ToAlert()
        {
            return _builder.ToAlert(FluentAlertTypes.Table);
        }
    }

    public class FluentTextBlockBuilder : IAlertable
    {
        private readonly FluentAlertBuilder _builder;
        public FluentTextBlockBuilder(FluentAlertBuilder builder)
        {
            _builder = builder;
        }
        public FluentTextBlockBuilder WithText(string text)
        {
            return With(_builder.Text(text));
        }

        public FluentTextBlockBuilder WithItalic(string text)
        {
            return With(_builder.Italic(text));
        }

        public FluentTextBlockBuilder WithUnderscore(string text)
        {
            return With(_builder.Underscore(text));
        }

        public FluentTextBlockBuilder WithStrong(string text)
        {
            return With(_builder.Strong(text));
        }

        public FluentTextBlockBuilder WithNewLine()
        {
            return With(_builder.NewLine());
        }

        private FluentTextBlockBuilder With(Alert alert)
        {
            _builder.Add(alert);
            return this;
        }

        public Alert ToAlert()
        {
            return _builder.ToAlert(FluentAlertTypes.TextBlock);
        }
    }
}
