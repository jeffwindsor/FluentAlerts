﻿using FluentAlerts.Builders;
using FluentAlerts.Domain;
using FluentAlerts.Extensions;

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

        public FluentTextBlockBuilder HeaderTextBlock(int level)
        {
            return new FluentTextBlockBuilder(level);
        }

        public FluentTextBlockBuilder HeaderTextBlock(string text, int level)
        {
            return HeaderTextBlock(level).WithNormal(text);
        }

        public OrderedList OrderedList(params object[] items)
        {
            return new OrderedList(items.ToOrderedListItems());
        }

        public UnOrderedList UnOrderedList(params object[] items)
        {
            return new UnOrderedList(items.ToUnOrderedListItems());
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

        public IFluentAlertSerializer Serializer<TTemplate>()
            where TTemplate : FluentAlertSerializerTemplate, new()
        {
            return new FluentAlertSerializer<TTemplate>(new TTemplate());
        }

    }
}
