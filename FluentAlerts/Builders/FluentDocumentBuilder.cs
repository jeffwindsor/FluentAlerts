using System.Collections.Generic;
using FluentAlerts.Domain;

namespace FluentAlerts.Builders
{
    public class FluentDocumentBuilder
    {
        private readonly List<object> _items = new List<object>();
        private readonly IFluentAlerts _factory;

        public FluentDocumentBuilder(IFluentAlerts factory)
        {
            _factory = factory;
        }

        public FluentDocumentBuilder WithHorizontalRule()
        {
            return With(new HorizontalRule());
        }

        public FluentDocumentBuilder WithHeader(string text, int level = 1)
        {
            return With(_factory.HeaderTextBlock(text, level).ToTextBlock());
        }

        public FluentDocumentBuilder WithLink(string url, string text = "")
        {
            return With(_factory.Link(url,text));
        }

        public FluentDocumentBuilder WithOrderedList(params object[] items)
        {
            return With(_factory.OrderedList(items));
        }

        public FluentDocumentBuilder WithUnOrderedList(params object[] items)
        {
            return With(_factory.UnOrderedList(items));
        }

        public FluentDocumentBuilder WithTextBlock(string text)
        {
            //var tb = new FluentTextBlockBuilder();
            return With(_factory.TextBlock(text).ToTextBlock());
        }
        
        public FluentDocumentBuilder WithCodeBlock(string language, string code)
        {
            return With(_factory.CodeBlock(language,code));
        }

        public FluentDocumentBuilder With<T>(T item)
        {
            _items.Add(item);
            return this;
        }
        
        public Document ToDocument()
        {
            return new Document(_items);
        }
    }
}